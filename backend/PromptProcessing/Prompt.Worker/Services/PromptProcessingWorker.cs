using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Prompt.Domain.Enums;
using Prompt.Infrastructure.Persistence;
using Prompt.Worker.Options;

namespace Prompt.Worker;

public class PromptProcessingWorker : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<PromptProcessingWorker> _logger;
    private readonly WorkerOptions _options;

    public PromptProcessingWorker(
        IServiceProvider serviceProvider,
        ILogger<PromptProcessingWorker> logger,
        IOptions<WorkerOptions> options
        )
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
        _options = options.Value;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Prompt Worker started.");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();

                var context = scope.ServiceProvider.GetRequiredService<PromptDbContext>();

                var canConnect = false;

                while (!canConnect && !stoppingToken.IsCancellationRequested)
                {
                    try
                    {
                        canConnect = await context.Database.CanConnectAsync(stoppingToken);
                    }
                    catch
                    {
                        _logger.LogWarning("Waiting for PostgreSQL...");
                        await Task.Delay(2000, stoppingToken);
                    }
                }

                var llm = scope.ServiceProvider.GetRequiredService<ILlmService>();

                var prompt = await context.PromptJobs
                    .Where(x => x.Status == PromptStatus.Pending)
                    .OrderBy(x => x.CreatedAtUtc)
                    .FirstOrDefaultAsync(stoppingToken);

                if (prompt != null)
                {
                    _logger.LogInformation(
                        "Processing prompt {Id}",
                        prompt.Id);

                    prompt.Status = PromptStatus.Processing;
                    prompt.StartedAtUtc = DateTimeOffset.UtcNow;

                    await context.SaveChangesAsync(stoppingToken);

                    try
                    {
                        var result = await llm.GenerateAsync(prompt.Prompt, stoppingToken);

                        prompt.Response = result;
                        prompt.Status = PromptStatus.Completed;
                        prompt.CompletedAtUtc = DateTimeOffset.UtcNow;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error while processing prompt");
                        prompt.Status = PromptStatus.Failed;
                        prompt.ErrorMessage = ex.Message;
                    }

                    await context.SaveChangesAsync(stoppingToken);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Worker error");
            }

            await Task.Delay(
                TimeSpan.FromSeconds(_options.PollingIntervalSeconds),
                stoppingToken);
        }
    }
}
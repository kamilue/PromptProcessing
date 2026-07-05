using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Prompt.Infrastructure.Services;
using Prompt.Worker.Options;
using Prompt.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using Prompt.Infrastructure.Persistence;
using Prompt.Worker;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddInfrastructure(context.Configuration);

        services.Configure<WorkerOptions>(context.Configuration.GetSection("WorkerOptions"));
        services.AddHostedService<PromptProcessingWorker>();
    })
    .Build();


await host.RunAsync();
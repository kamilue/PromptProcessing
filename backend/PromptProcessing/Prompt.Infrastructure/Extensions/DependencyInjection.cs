using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Prompt.Infrastructure.Persistence;
using Prompt.Application.Interfaces;
using Prompt.Infrastructure.Services;
using Prompt.Application.Interfaces;
using Prompt.Infrastructure.Services;

namespace Prompt.Infrastructure.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<PromptDbContext>(options =>
        {
            options.UseNpgsql(
                configuration.GetConnectionString("Postgres"));
        });
        services.AddScoped<IPromptService, PromptService>();

        services.AddHttpClient<ILlmService, OllamaLlmService>(client =>
        {
            client.BaseAddress = new Uri("http://ollama:11434");
            client.Timeout = TimeSpan.FromMinutes(10);
        });

        return services;
    }
}
using System.Net.Http.Json;
using Prompt.Application.Interfaces;

namespace Prompt.Infrastructure.Services;

public class OllamaLlmService : ILlmService
{
    private readonly HttpClient _http;

    public OllamaLlmService(HttpClient http)
    {
        _http = http;
    }

    public async Task<string> GenerateAsync(string prompt, CancellationToken ct)
    {
        Console.WriteLine("Sending request to Ollama...");

        var request = new
        {
            model = "llama3.2",
            prompt = prompt,
            stream = false,
            keep_alive = "30m"
        };

        var response = await _http.PostAsJsonAsync(
            "/api/generate",
            request,
            ct);

        Console.WriteLine($"Status: {response.StatusCode}");

        response.EnsureSuccessStatusCode();

        var result = await response.Content
            .ReadFromJsonAsync<OllamaResponse>(cancellationToken: ct);

        return result?.response ?? "";
    }

    private class OllamaResponse
    {
        public string response { get; set; } = "";
    }
}

using Prompt.Domain.Enums;

namespace Prompt.Api.Contracts;

public class PromptResponseDto
{
    public Guid Id { get; set; }
    public string Prompt { get; set; } = string.Empty;
    public string? Response { get; set; }
    public PromptStatus Status { get; set; }

    public DateTimeOffset CreatedAtUtc { get; set; }
    public DateTimeOffset? StartedAtUtc { get; set; }
    public DateTimeOffset? CompletedAtUtc { get; set; }

    public string? ErrorMessage { get; set; }
}
using Prompt.Domain.Enums;

namespace Prompt.Domain.Entities;

public class PromptJob
{
    public Guid Id { get; set; }

    public string Prompt { get; set; } = string.Empty;

    public string? Response { get; set; }

    public PromptStatus Status { get; set; } = PromptStatus.Pending;

    public DateTimeOffset CreatedAtUtc { get; set; }

    public DateTimeOffset? StartedAtUtc { get; set; }

    public DateTimeOffset? CompletedAtUtc { get; set; }

    public string? ErrorMessage { get; set; }
}
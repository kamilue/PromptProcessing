using Prompt.Api.Contracts;
using Prompt.Domain.Entities;

namespace Prompt.Api.Mapping;

public static class PromptMapping
{
    public static PromptResponseDto ToDto(this PromptJob job)
    {
        return new PromptResponseDto
        {
            Id = job.Id,
            Prompt = job.Prompt,
            Response = job.Response,
            Status = job.Status,
            CreatedAtUtc = job.CreatedAtUtc,
            StartedAtUtc = job.StartedAtUtc,
            CompletedAtUtc = job.CompletedAtUtc,
            ErrorMessage = job.ErrorMessage
        };
    }
}
using Prompt.Application.DTOs;

namespace Prompt.Application.Interfaces;

public interface IPromptService
{
    Task<PromptDto> CreateAsync(CreatePromptRequest request);

    Task<List<PromptDto>> GetAllAsync();
    Task<PromptDto> GetByIdAsync(Guid id);
}
namespace Prompt.Application.Interfaces;

public interface IPromptNotificationService
{
    Task PromptCreated(Guid promptId);

    Task PromptUpdated(Guid promptId);
}
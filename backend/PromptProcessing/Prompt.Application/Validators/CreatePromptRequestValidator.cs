using FluentValidation;
using Prompt.Application.DTOs;

namespace Prompt.Application.Validators;

public class CreatePromptRequestValidator : AbstractValidator<CreatePromptRequest>
{
    public CreatePromptRequestValidator()
    {
        RuleFor(x => x.Prompt)
            .NotEmpty()
            .MaximumLength(4000);
    }
}
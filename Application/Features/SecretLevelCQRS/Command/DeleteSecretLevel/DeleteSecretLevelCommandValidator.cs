using Application.Helpers;
using Application.Resource;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.Features.SecretLevelCQRS.Command.DeleteSecretLevel;

public class DeleteSecretLevelCommandValidator : AbstractValidator<DeleteSecretLevelCommand>
{
    public DeleteSecretLevelCommandValidator(IStringLocalizer<Localize> localizer)
    {
        RuleFor(p => p.Id)
            .GreaterThan(ValidationHelpers.SecretLevelMax).WithMessage(x => localizer["SystemDataError"])
            .NotNull().WithMessage(x => localizer["Required"])
            .OverridePropertyName("Id");
    }
}
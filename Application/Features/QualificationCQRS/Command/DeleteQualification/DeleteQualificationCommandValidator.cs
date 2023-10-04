using Application.Helpers;
using Application.Resource;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.Features.QualificationCQRS.Command.DeleteQualification;

public class DeleteQualificationCommandValidator : AbstractValidator<DeleteQualificationCommand>
{
    public DeleteQualificationCommandValidator(IStringLocalizer<Localize> localizer)
    {
        RuleFor(p => p.Id)
            .NotNull().WithMessage(x => localizer["Required"])
            .GreaterThan(ValidationHelpers.QualificationStatusMax).WithMessage(x => localizer["SystemDataError"])
            .OverridePropertyName("Id");
    }
}
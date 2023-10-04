using Application.Helpers;
using Application.Resource;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.Features.EducationCQRS.Command.DeleteEducation;

public class DeleteEducationCommandValidator : AbstractValidator<DeleteEducationCommand>
{
    public DeleteEducationCommandValidator(IStringLocalizer<Localize> localizer)
    {
        RuleFor(p => p.Id)
            .NotNull().WithMessage(x => localizer["Required"])
            .GreaterThan(ValidationHelpers.EducationMax).WithMessage(x => localizer["SystemDataError"])
            .OverridePropertyName("Id");
    }
}
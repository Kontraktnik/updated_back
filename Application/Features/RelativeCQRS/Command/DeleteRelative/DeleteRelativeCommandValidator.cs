using Application.Helpers;
using Application.Resource;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.Features.RelativeCQRS.Command.DeleteRelative;

public class DeleteRelativeCommandValidator : AbstractValidator<DeleteRelativeCommand>
{
    public DeleteRelativeCommandValidator(IStringLocalizer<Localize> localizer)
    {
        RuleFor(p => p.Id)
            .NotNull().WithMessage(x => localizer["Required"])
            .GreaterThan(ValidationHelpers.RelativeMax).WithMessage(x => localizer["SystemDataError"])
            .OverridePropertyName("Id");
    }
}
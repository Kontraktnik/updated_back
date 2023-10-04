using Application.Helpers;
using Application.Resource;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.Features.CategoryPositionCQRS.Command.DeleteCategoryPosition;

public class DeleteCategoryPositionCommandValidator : AbstractValidator<DeleteCategoryPositionCommand>
{
    public DeleteCategoryPositionCommandValidator(IStringLocalizer<Localize> localizer)
    {
        RuleFor(p => p.Id)
            .NotNull().WithMessage(x => localizer["Required"])
            .GreaterThan(ValidationHelpers.CategoryPositionMax).WithMessage(x => localizer["SystemDataError"])
            .OverridePropertyName("Id");
    }
}
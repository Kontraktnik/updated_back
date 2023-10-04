using Application.Helpers;
using Application.Resource;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.Features.JobCategoryCQRS.Command.DeleteJobCategory;

public class DeleteJobCategoryCommandValidator : AbstractValidator<DeleteJobCategoryCommand>
{
    public DeleteJobCategoryCommandValidator(IStringLocalizer<Localize> localizer)
    {
        RuleFor(p => p.Id)
            .NotNull().WithMessage(x => localizer["Required"])
            .GreaterThan(ValidationHelpers.JobCategoryMax).WithMessage(x => localizer["SystemDataError"])
            .OverridePropertyName("Id");
    }
}
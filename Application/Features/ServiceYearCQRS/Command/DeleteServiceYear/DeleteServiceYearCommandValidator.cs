using Application.Helpers;
using Application.Resource;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.Features.ServiceYearCQRS.Command.DeleteServiceYear;

public class DeleteServiceYearCommandValidator : AbstractValidator<DeleteServiceYearCommand>
{
    public DeleteServiceYearCommandValidator(IStringLocalizer<Localize> localizer)
    {
        RuleFor(p => p.Id)
            .NotNull().WithMessage(x => localizer["Required"])
            .GreaterThan(ValidationHelpers.ServiceYearMax).WithMessage(x => localizer["SystemDataError"])
            .OverridePropertyName("Id");
    }
}
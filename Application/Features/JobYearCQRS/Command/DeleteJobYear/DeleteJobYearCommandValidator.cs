using Application.Helpers;
using Application.Resource;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.Features.JobYearCQRS.Command.DeleteJobYear;

public class DeleteJobYearCommandValidator : AbstractValidator<DeleteJobYearCommand>
{
    public DeleteJobYearCommandValidator(IStringLocalizer<Localize> localizer)
    {
        RuleFor(p => p.Id)
            .NotNull().WithMessage(x => localizer["Required"])
            .OverridePropertyName("Id");
    }
}
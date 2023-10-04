using Application.Resource;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.Features.RankSalaryCQRS.Command.DeleteRankSalary;

public class DeleteRankSalaryCommandValidator : AbstractValidator<DeleteRankSalaryCommand>
{
    public DeleteRankSalaryCommandValidator(IStringLocalizer<Localize> localizer)
    {
        RuleFor(p => p.Id)
            .NotNull().WithMessage(x => localizer["Required"])
            .OverridePropertyName("Id");
    }
}
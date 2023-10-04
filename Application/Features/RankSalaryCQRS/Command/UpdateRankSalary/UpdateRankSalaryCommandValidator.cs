using Application.Resource;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.Features.RankSalaryCQRS.Command.UpdateRankSalary;

public class UpdateRankSalaryCommandValidator : AbstractValidator<UpdateRankSalaryCommand>
{
    public UpdateRankSalaryCommandValidator(IStringLocalizer<Localize> localizer)
    {
        RuleFor(p => p)
            .Must(p => p.Id == p.model.Id).WithMessage(x => localizer["NotEmpty"])
            .OverridePropertyName("Id");
        RuleFor(p => p.model.ArmyRankId)
            .GreaterThan(0).WithMessage(x => localizer["NotValid"])
            .NotEmpty().WithMessage(x => localizer["NotEmpty"])
            .OverridePropertyName("ArmyRankId");
        RuleFor(p => p.model.Salary)
            .NotEmpty().WithMessage(x => localizer["NotEmpty"])
            .GreaterThan(0).WithMessage(x => localizer["NotValid"])
            .OverridePropertyName("Salary");
    }
}
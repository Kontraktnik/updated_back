using Application.Resource;
using Domain.Models.CalculationModels;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.Features.RankSalaryCQRS.Command.AddRankSalary;

public class AddRankSalaryValidator : AbstractValidator<AddRankSalaryCommand>
{
    public AddRankSalaryValidator(IStringLocalizer<Localize> localizer)
    {
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
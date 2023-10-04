using Application.Resource;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.Features.CalculationCQRS.Query.CalculateSalaryAsync;

public class CalculateSalaryAsyncQueryValidator : AbstractValidator<CalculateSalaryAsyncQuery>
{
    public CalculateSalaryAsyncQueryValidator(IStringLocalizer<Localize> localizer)
    {
        RuleFor(p => p.model.PositionId)
            .NotEmpty().WithMessage(x => localizer["Required"])
            .GreaterThan(0).WithMessage(x => localizer["NotValid"])
            .OverridePropertyName("PositionId");
        RuleFor(p => p.model.ArmyRankId)
            .NotEmpty().WithMessage(x => localizer["Required"])
            .GreaterThan(0).WithMessage(x => localizer["NotValid"])
            .OverridePropertyName("ArmyRankId");
        RuleFor(p => p.model.QualificationId)
            .NotEmpty().WithMessage(x => localizer["Required"])
            .GreaterThan(0).WithMessage(x => localizer["NotValid"])
            .OverridePropertyName("QualificationId");
        RuleFor(p => p.model.ServiceYearId)
            .NotEmpty().WithMessage(x => localizer["Required"])
            .GreaterThan(0).WithMessage(x => localizer["NotValid"])
            .OverridePropertyName("ServiceYearId");
    }
}
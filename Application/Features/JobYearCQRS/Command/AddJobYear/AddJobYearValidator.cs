using Application.Resource;
using Domain.Models.CalculationModels;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.Features.JobYearCQRS.Command.AddJobYear;

public class AddJobYearValidator : AbstractValidator<AddJobYearCommand>
{
    public AddJobYearValidator(IStringLocalizer<Localize> localizer)
    {
        RuleFor(p => p.model.JobCategoryId)
            .NotEmpty().WithMessage(x => localizer["NotEmpty"])
            .GreaterThan(0).WithMessage(x => localizer["NotValid"])
            .OverridePropertyName("JobCategoryId");
        RuleFor(p => p.model.ServiceYearId)
            .NotEmpty().WithMessage(x => localizer["NotEmpty"])
            .GreaterThan(0).WithMessage(x => localizer["NotValid"])
            .OverridePropertyName("ServiceYearId");
        RuleFor(p => p.model.Salary)
            .NotEmpty().WithMessage(x => localizer["NotEmpty"])
            .GreaterThan(-1).WithMessage(x => localizer["NotValid"])
            .OverridePropertyName("Salary");
    }
}
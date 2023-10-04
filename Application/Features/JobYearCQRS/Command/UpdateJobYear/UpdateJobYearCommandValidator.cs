using Application.Resource;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.Features.JobYearCQRS.Command.UpdateJobYear;

public class UpdateJobYearCommandValidator : AbstractValidator<UpdateJobYearCommand>
{
    public UpdateJobYearCommandValidator(IStringLocalizer<Localize> localizer)
    {
        RuleFor(p => p)
            .Must(p => p.Id == p.model.Id).WithMessage(x => localizer["NotValid"])
            .OverridePropertyName("Id");
        RuleFor(p => p.model.JobCategoryId)
            .GreaterThan(0).WithMessage(x => localizer["NotValid"])
            .NotEmpty().WithMessage(x => localizer["NotEmpty"])
            .OverridePropertyName("JobCategoryId");
        RuleFor(p => p.model.ServiceYearId)
            .GreaterThan(0).WithMessage(x => localizer["NotValid"])
            .NotEmpty().WithMessage(x => localizer["NotEmpty"])
            .OverridePropertyName("ServiceYearId");
        RuleFor(p => p.model.Salary)
            .NotEmpty().WithMessage(x => localizer["NotEmpty"])
            .GreaterThan(-1).WithMessage(x => localizer["NotValid"])
            .OverridePropertyName("Salary");
        
    }
}
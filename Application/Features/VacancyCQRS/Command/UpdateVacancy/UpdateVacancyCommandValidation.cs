using Application.Resource;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.Features.VacancyCQRS.Command.UpdateVacancy;

public class UpdateVacancyCommandValidation : AbstractValidator<UpdateVacancyCommand>
{
    public UpdateVacancyCommandValidation(IStringLocalizer<Localize> localizer)
    {
        RuleFor(p => p.Id)
            .NotEmpty().WithMessage(x => localizer["NotValid"])
            .OverridePropertyName("Id");
        RuleFor(p => p.model.DescriptionRu)
            .NotEmpty().WithMessage(x => localizer["NotEmpty"])
            .OverridePropertyName("DescriptionRu");
        RuleFor(p => p.model.PositionId)
            .NotEmpty().WithMessage(x => localizer["NotEmpty"])
            .GreaterThan(0).WithMessage(x => localizer["NotValid"])
            .OverridePropertyName("PositionId");
        RuleFor(p => p.model.JobCategoryId)
            .NotEmpty().WithMessage(x => localizer["NotEmpty"])
            .GreaterThan(0).WithMessage(x => localizer["NotValid"])
            .OverridePropertyName("JobCategoryId");
        RuleFor(p => p.model.City)
            .NotEmpty().WithMessage(x => localizer["NotEmpty"])
            .OverridePropertyName("City");
        RuleFor(p => p.model.ArmyTypeId)
            .NotEmpty().WithMessage(x => localizer["NotEmpty"])
            .GreaterThan(0).WithMessage(x => localizer["NotValid"])
            .OverridePropertyName("ArmyTypeId");
        RuleFor(p => p.model.ArmyRegionId)
            .NotEmpty().WithMessage(x => localizer["NotEmpty"])
            .GreaterThan(0).WithMessage(x => localizer["NotValid"])
            .OverridePropertyName("ArmyRegionId");
        RuleFor(p => p.model.SecretLevelId)
            .NotEmpty().WithMessage(x => localizer["NotEmpty"])
            .GreaterThan(0).WithMessage(x => localizer["NotValid"])
            .OverridePropertyName("SecretLevelId");
        RuleFor(p => p.model.QualificationId)
            .NotEmpty().WithMessage(x => localizer["NotEmpty"])
            .GreaterThan(0).WithMessage(x => localizer["NotValid"])
            .OverridePropertyName("QualificationId");
        RuleFor(p => p.model.Quantity)
            .NotEmpty().WithMessage("It cant be empty")
            .GreaterThan(0).WithMessage(x => localizer["NotValid"])
            .OverridePropertyName("Quantity");
        RuleFor(p => p.model.Status)
            .NotEmpty().WithMessage(x => localizer["NotEmpty"])
            .OverridePropertyName("Status");
    }
}
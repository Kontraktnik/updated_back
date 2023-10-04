using Application.Resource;
using Domain.Models.CalculationModels;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.Features.PositionCQRS.Command.AddPosition;

public class AddPositionValidator : AbstractValidator<AddPositionCommand>
{
    public AddPositionValidator(IStringLocalizer<Localize> localizer)
    {
        RuleFor(p => p.model.JobCategoryId)
            .GreaterThan(0).WithMessage(x => localizer["NotValid"])
            .NotEmpty().WithMessage(x => localizer["NotEmpty"])
            .OverridePropertyName("JobCategoryId");
        RuleFor(p => p.model.SecretLevelId)
            .GreaterThan(0).WithMessage(x => localizer["NotValid"])
            .NotEmpty().WithMessage(x => localizer["NotEmpty"])
            .OverridePropertyName("SecretLevelId");
        RuleFor(p => p.model.TitleEn)
            .NotEmpty().WithMessage(x => localizer["NotEmpty"])
            .OverridePropertyName("TitleEn");
        RuleFor(p => p.model.TitleRu)
            .NotEmpty().WithMessage(x => localizer["NotEmpty"])
            .OverridePropertyName("TitleRu");
        RuleFor(p => p.model.TitleKz)
            .NotEmpty().WithMessage(x => localizer["NotEmpty"])
            .OverridePropertyName("TitleKz");
    }
}
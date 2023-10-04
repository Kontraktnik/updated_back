using Application.Resource;
using Domain.Models.CalculationModels;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.Features.SecretLevelCQRS.Command.AddSecretLevel;

public class AddSecretLevelValidator : AbstractValidator<AddSecretLevelCommand>
{
    public AddSecretLevelValidator(IStringLocalizer<Localize> localizer)
    {
        RuleFor(p => p.model.TitleEn)
            .NotEmpty().WithMessage(x => localizer["NotEmpty"])
            .OverridePropertyName("TitleEn");
        RuleFor(p => p.model.TitleRu)
            .NotEmpty().WithMessage(x => localizer["NotEmpty"])
            .OverridePropertyName("TitleRu");
        RuleFor(p => p.model.TitleKz)
            .NotEmpty().WithMessage(x => localizer["NotEmpty"])
            .OverridePropertyName("TitleKz");
        RuleFor(p => p.model.Percentage)
            .NotEmpty().WithMessage(x => localizer["NotEmpty"])
            .InclusiveBetween(-1,101).WithMessage(x => localizer["NotValid"])
            .OverridePropertyName("Percentage");
    }
}
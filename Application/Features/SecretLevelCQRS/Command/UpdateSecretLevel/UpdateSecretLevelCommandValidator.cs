using Application.Resource;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.Features.SecretLevelCQRS.Command.UpdateSecretLevel;

public class UpdateSecretLevelCommandValidator : AbstractValidator<UpdateSecretLevelCommand>
{
    public UpdateSecretLevelCommandValidator(IStringLocalizer<Localize> localizer)
    {
        RuleFor(p => p)
            .Must(p => p.Id == p.model.Id).WithMessage(x => localizer["NotValid"])
            .OverridePropertyName("Id");
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
            .InclusiveBetween(-1,101).WithMessage(x => localizer["Percentage_Rules"])
            .OverridePropertyName("Percentage");
    }
}
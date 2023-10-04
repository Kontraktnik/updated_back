using Application.Resource;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.Features.MedicalStatusCQRS.Command.AddMedicalStatus;

public class AddMedicalStatusCommandValidator : AbstractValidator<AddMedicalStatusCommand>
{
    public AddMedicalStatusCommandValidator(IStringLocalizer<Localize> localizer)
    {
        RuleFor(p => p.model.Code)
            .NotEmpty().WithMessage(x => localizer["NotEmpty"])
            .OverridePropertyName("Code");
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
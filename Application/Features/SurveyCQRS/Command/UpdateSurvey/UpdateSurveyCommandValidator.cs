using Application.Resource;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.Features.SurveyCQRS.Command.UpdateSurvey;

public class UpdateSurveyCommandValidator : AbstractValidator<UpdateSurveyCommand>
{
    public UpdateSurveyCommandValidator(IStringLocalizer<Localize> localizer)
    {
        RuleFor(p => p.model.TuberUrl)
            .NotNull().WithMessage(x => localizer["NotNull"])
            .OverridePropertyName("TuberUrl");
        RuleFor(p => p.model.NarcologicalUrl)
            .NotNull().WithMessage(x => localizer["NotNull"])
            .OverridePropertyName("NarcologicalUrl");
        RuleFor(p => p.model.PsychoNeurologicalUrl)
            .NotNull().WithMessage(x => localizer["NotNull"])
            .OverridePropertyName("PsychoNeurologicalUrl");
        RuleFor(p => p.model.DermatologUrl)
            .NotNull().WithMessage(x => localizer["NotNull"])
            .OverridePropertyName("DermatologUrl");
    }
}
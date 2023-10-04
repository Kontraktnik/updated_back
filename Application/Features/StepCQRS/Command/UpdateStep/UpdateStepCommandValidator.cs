using Application.Resource;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.Features.StepCQRS.Command.UpdateStep;

public class UpdateStepCommandValidator : AbstractValidator<UpdateStepCommand>
{
    public UpdateStepCommandValidator(IStringLocalizer<Localize> localizer)
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
        RuleFor(p => p.model.DayLimit)
            .NotEmpty().WithMessage("It cant be empty")
            .InclusiveBetween(0,30).WithMessage(x => localizer["NotValid"])
            .OverridePropertyName("DayLimit");
    }
}
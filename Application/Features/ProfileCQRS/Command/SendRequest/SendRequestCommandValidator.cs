using Application.Resource;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.Features.ProfileCQRS.Command.SendRequest;

public class SendRequestCommandValidator : AbstractValidator<SendRequestCommand>
{
    public SendRequestCommandValidator(IStringLocalizer<Localize> localizer)
    {
        RuleFor(p => p.model.StepId)
            .GreaterThan(0).WithMessage(x => localizer["NotValid"])
            .NotNull().WithMessage(x => localizer["NotValid"])
            .OverridePropertyName("StepId");
        RuleFor(p => p.model.SurveyId)
            .GreaterThan(0).WithMessage(x => localizer["NotValid"])
            .NotNull().WithMessage(x => localizer["NotValid"])
            .OverridePropertyName("SurveyId");
        RuleFor(p => p.model.Status)
            .Must(p=>p.Equals(-1) || p.Equals(1)).WithMessage(x => localizer["NotValid"])
            .NotNull().WithMessage(x => localizer["NotValid"])
            .OverridePropertyName("Status");
    }
}
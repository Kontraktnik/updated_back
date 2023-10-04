using Application.Resource;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.Features.ProfileCQRS.Command.SendConfirmation;

public class SendConfirmationCommandValidator : AbstractValidator<SendConfirmationCommand>
{
    public SendConfirmationCommandValidator(IStringLocalizer<Localize> localizer)
    {
        RuleFor(p => p.CurrentUser)
            .NotNull().WithMessage(x => localizer["NotValid"])
            .OverridePropertyName("User");
        RuleFor(p => p.ProfileId)
            .GreaterThan(0).WithMessage(x => localizer["NotValid"])
            .NotNull().WithMessage(x => localizer["NotValid"])
            .OverridePropertyName("ProfileId");
        RuleFor(p => p.model.Status)
            .Must(p=>p.Equals(-1) || p.Equals(1)).WithMessage(x => localizer["NotValid"])
            .NotNull().WithMessage(x => localizer["NotValid"])
            .OverridePropertyName("Status");
    }
}
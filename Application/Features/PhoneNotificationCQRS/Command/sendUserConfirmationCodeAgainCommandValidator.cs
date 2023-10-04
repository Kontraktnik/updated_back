using Application.Resource;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.Features.PhoneNotificationCQRS.Command;

public class sendUserConfirmationCodeAgainCommandValidator : AbstractValidator<sendUserConfirmationCodeAgainCommand>
{
    public sendUserConfirmationCodeAgainCommandValidator(IStringLocalizer<Localize> localizer)
    {
        RuleFor(r => r.IIN)
            .NotEmpty().WithMessage(x => localizer["Required"])
            .Must(u=>u.Length == 12).WithMessage(x => localizer["IIN_Number"])
            .Matches(@"^[0-9]*$").WithMessage(x => localizer["IIN_Number"])
            .OverridePropertyName("IIN");
    }
}
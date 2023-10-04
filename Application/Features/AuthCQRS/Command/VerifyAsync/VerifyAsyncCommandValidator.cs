using Application.Features.AuthCQRS.Command.RegisterAsync;
using Application.Resource;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.Features.AuthCQRS.Command.VerifyAsync;

public class VerifyAsyncCommandValidator : AbstractValidator<VerifyAsyncCommand>
{
    public VerifyAsyncCommandValidator(IStringLocalizer<Localize> localizer)
    {
        RuleFor(v=>v.verifyRegistrationDto.IIN)
            .NotEmpty().WithMessage(x => localizer["Required"])
            .Must(u=>u.Length == 12).WithMessage(x => localizer["IIN_Number"])
            .Matches(@"^[0-9]*$").WithMessage(x => localizer["IIN_Number"])
            .OverridePropertyName("IIN");
        RuleFor(v=>v.verifyRegistrationDto.Code)
            .Must(u=>u.Length == 6).WithMessage(x => localizer["FixedLength"] + "6")
            .Matches(@"^[0-9]*$").WithMessage(x => localizer["OnlyNumber"])
            .OverridePropertyName("Code");
    }
}
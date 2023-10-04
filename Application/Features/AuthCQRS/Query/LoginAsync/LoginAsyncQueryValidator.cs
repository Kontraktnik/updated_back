using Application.Resource;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.Features.AuthCQRS.Query.LoginAsync;

public class LoginAsyncQueryValidator : AbstractValidator<LoginAsyncQuery>
{
    public LoginAsyncQueryValidator(IStringLocalizer<Localize> localizer)
    {
        RuleFor(r => r.credentials.IIN)
            .NotEmpty().WithMessage("IIN required")
            .Must(u=>u.Length == 12).WithMessage(x => localizer["IIN_Number"])
            .Matches(@"^[0-9]*$").WithMessage(x => localizer["IIN_Number"])
            .OverridePropertyName("IIN");
        RuleFor(r => r.credentials.Password)
            .NotEmpty().WithMessage(x => localizer["Required"])
            .Matches(@"^(?=.*[0-9]+.*)(?=.*[a-zA-Z]+.*)[0-9a-zA-Z@*_.]{6,}$").WithMessage(x => localizer["Password_Rules"])
            .OverridePropertyName("Password");
    }
}
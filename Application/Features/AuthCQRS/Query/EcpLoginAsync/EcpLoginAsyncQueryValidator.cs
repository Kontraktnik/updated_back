using Application.Resource;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.Features.AuthCQRS.Query.EcpLoginAsync;

public class EcpLoginAsyncQueryValidator : AbstractValidator<EcpLoginAsyncQuery>
{
    public EcpLoginAsyncQueryValidator(IStringLocalizer<Localize> localizer)
    {
        RuleFor(r => r.credentials.IIN)
            .NotEmpty().WithMessage("IIN required")
            .Must(u=>u.Length == 12).WithMessage(x => localizer["IIN_Number"])
            .Matches(@"^[0-9]*$").WithMessage(x => localizer["IIN_Number"])
            .OverridePropertyName("IIN");
    }
}
using Application.Helpers;
using Application.Resource;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.Features.DriverLicenseCQRS.Command.DeleteDriverLicense;

public class DeleteDriverLicenseCommandValidator : AbstractValidator<DeleteDriverLicenseCommand>
{
    public DeleteDriverLicenseCommandValidator(IStringLocalizer<Localize> localizer)
    {
        RuleFor(p => p.Id)
            .NotNull().WithMessage(x => localizer["Required"])
            .GreaterThan(ValidationHelpers.DriverLicenseMax).WithMessage(x => localizer["SystemDataError"])
            .OverridePropertyName("Id");
    }
}
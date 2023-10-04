using Application.Helpers;
using Application.Resource;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.Features.MedicalStatusCQRS.Command.DeleteMedicalStatus;

public class DeleteMedicalStatusCommandValidator : AbstractValidator<DeleteMedicalStatusCommand>
{
    public DeleteMedicalStatusCommandValidator(IStringLocalizer<Localize> localizer)
    {
        RuleFor(p => p.Id)
            .NotNull().WithMessage(x => localizer["Required"])
            .GreaterThan(ValidationHelpers.MedicalStatusMax).WithMessage(x => localizer["SystemDataError"])
            .OverridePropertyName("Id");
    }
}
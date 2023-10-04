using Application.Helpers;
using Application.Resource;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.Features.ArmyDepartmentCQRS.Command.DeleteArmyDepartment;

public class DeleteArmyDepartmentCommandValidator : AbstractValidator<DeleteArmyDepartmentCommand>
{
    public DeleteArmyDepartmentCommandValidator(IStringLocalizer<Localize> localizer)
    {
        RuleFor(v => v.Id)
            .NotEmpty().WithMessage(x => localizer["Required"])
            .GreaterThan(ValidationHelpers.ArmyDepartmentMax).WithMessage(x => localizer["SystemDataError"])
            .OverridePropertyName("Id");
    }
}
using Application.Helpers;
using Application.Resource;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.Features.ArmyRegionCQRS.Command.DeleteArmyRegion;

public class DeleteArmyRegionCommandValidator : AbstractValidator<DeleteArmyRegionCommand>
{
    public DeleteArmyRegionCommandValidator(IStringLocalizer<Localize> localizer)
    {
        RuleFor(v => v.Id)
            .GreaterThan(ValidationHelpers.ArmyRegionMax).WithMessage(x => localizer["SystemDataError"])
            .NotEmpty().WithMessage(x => localizer["Required"])
            .OverridePropertyName("Id");
    }
}
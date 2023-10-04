using Application.Helpers;
using Application.Resource;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.Features.ArmyRankCQRS.Command.DeleteArmyRank;

public class DeleteArmyRankCommandValidator : AbstractValidator<DeleteArmyRankCommand>
{
    public DeleteArmyRankCommandValidator(IStringLocalizer<Localize> localizer)
    {
        RuleFor(v => v.Id)
            .NotEmpty().WithMessage(x => localizer["NotValid"])
            .GreaterThan(ValidationHelpers.ArmyRankMax).WithMessage(x => localizer["SystemDataError"])
            .OverridePropertyName("Id");

    }
}
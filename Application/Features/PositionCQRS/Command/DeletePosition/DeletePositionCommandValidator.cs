using Application.Resource;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.Features.PositionCQRS.Command.DeletePosition;

public class DeletePositionCommandValidator : AbstractValidator<DeletePositionCommand>
{
    public DeletePositionCommandValidator(IStringLocalizer<Localize> localizer)
    {
        RuleFor(p => p.Id)
            .NotNull().WithMessage(x => localizer["Required"])
            .OverridePropertyName("Id");
    }
}
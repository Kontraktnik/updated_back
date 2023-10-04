using Application.Helpers;
using Application.Resource;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.Features.AreaCQRS.Command.DeleteArea;

public class DeleteAreaCommandValidator : AbstractValidator<DeleteAreaCommand>
{
    public DeleteAreaCommandValidator(IStringLocalizer<Localize> localizer)
    {
        RuleFor(v => v.Id)
            .NotEmpty().WithMessage(x => localizer["Required"])
            .GreaterThan(ValidationHelpers.AreaMaxId).WithMessage(x => localizer["SystemDataError"])
            .OverridePropertyName("Id");
    }
}
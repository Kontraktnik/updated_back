using Application.Helpers;
using Application.Resource;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.Features.VTShCQRS.Command.DeleteVTSh;

public class DeleteVTShCommandValidator : AbstractValidator<DeleteVTShCommand>
{
    public DeleteVTShCommandValidator(IStringLocalizer<Localize> localizer)
    {
        RuleFor(v => v.Id)
            .NotEmpty().WithMessage(x => localizer["NotValid"])
            .GreaterThan(ValidationHelpers.VtshMax).WithMessage(x => localizer["NotValid"])
            .OverridePropertyName("Id");
    }
}
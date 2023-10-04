using Application.Resource;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.Features.VacancyCQRS.Command.DeleteVacancy;

public class DeleteVacancyCommandValidator : AbstractValidator<DeleteVacancyCommand>
{
    public DeleteVacancyCommandValidator(IStringLocalizer<Localize> localizer)
    {
        RuleFor(v => v.Id)
            .NotEmpty().WithMessage(x => localizer["NotValid"])
            .OverridePropertyName("Id");
    }
}
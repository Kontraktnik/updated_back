using Application.Resource;
using Domain.Models.CalculationModels;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.Features.CategoryPositionCQRS.Command.AddCategoryPosition;

public class AddCategoryPositionValidator : AbstractValidator<AddCategoryPositionCommand>
{
    public AddCategoryPositionValidator(IStringLocalizer<Localize> localizer)
    {
        RuleFor(p => p.model.TitleEn)
            .NotEmpty().WithMessage(x => localizer["NotEmpty"])
            .OverridePropertyName("TitleEn");
        RuleFor(p => p.model.TitleRu)
            .NotEmpty().WithMessage(x => localizer["NotEmpty"])
            .OverridePropertyName("TitleRu");
        RuleFor(p => p.model.TitleKz)
            .NotEmpty().WithMessage(x => localizer["NotEmpty"])
            .OverridePropertyName("TitleKz");
    }
}
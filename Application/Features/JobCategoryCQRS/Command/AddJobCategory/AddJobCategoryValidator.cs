using Application.Resource;
using Domain.Models.CalculationModels;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.Features.JobCategoryCQRS.Command.AddJobCategory;

public class AddJobCategoryValidator : AbstractValidator<AddJobCategoryCommand>
{
    public AddJobCategoryValidator(IStringLocalizer<Localize> localizer)
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
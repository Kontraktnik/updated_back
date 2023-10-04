using Application.Resource;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.Features.JobCategoryCQRS.Command.UpdateJobCategory;

public class UpdateJobCategoryCommandValidator : AbstractValidator<UpdateJobCategoryCommand>
{
    public UpdateJobCategoryCommandValidator(IStringLocalizer<Localize> localizer)
    {
        RuleFor(v => v)
            .Must(v=>v.Id == v.model.Id).WithMessage(x => localizer["NotValid"])
            .OverridePropertyName("Id");
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
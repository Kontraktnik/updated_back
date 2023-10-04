using Application.Resource;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.Features.ServiceYearCQRS.Command.UpdateServiceYear;

public class UpdateServiceYearCommandValidator : AbstractValidator<UpdateServiceYearCommand>
{
    public UpdateServiceYearCommandValidator(IStringLocalizer<Localize> localizer)
    {
        RuleFor(p => p)
            .Must(p => p.Id == p.model.Id).WithMessage(x => localizer["NotValid"])
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
        RuleFor(p => p.model)
            .Must(p=>p.Min < p.Max).WithMessage(x => localizer["NotValid"])
            .OverridePropertyName("Min");
        RuleFor(p => p.model)
            .Must(p=>p.Max > p.Min).WithMessage(x => localizer["NotValid"])
            .OverridePropertyName("Max");
        
    }
}
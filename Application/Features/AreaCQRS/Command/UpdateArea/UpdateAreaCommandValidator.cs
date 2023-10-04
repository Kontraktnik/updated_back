using Application.Resource;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.Features.AreaCQRS.Command.UpdateArea;

public class UpdateAreaCommandValidator : AbstractValidator<UpdateAreaCommand>
{
    public UpdateAreaCommandValidator(IStringLocalizer<Localize> localizer)
    {
        RuleFor(p => p.Id)
            .NotEmpty().WithMessage(x => localizer["NotNull"])
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
using Application.Resource;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.Features.SurveyExecutorCQRS.Command.AddSurveyExecutor;

public class AddSurveyExecutorCommandValidator : AbstractValidator<AddSurveyExecutorCommand>
{
    public AddSurveyExecutorCommandValidator(IStringLocalizer<Localize> localizer)
    {
        RuleFor(p => p.model.SurveyId)
            .NotNull().WithMessage(x => localizer["NotNull"])
            .GreaterThan(0).WithMessage(x => localizer["NotValid"])
            .OverridePropertyName("ExecutorId");
        
        RuleFor(p => p.model.DirectorId)
            .NotNull().WithMessage(x => localizer["NotNull"])
            .GreaterThan(0).WithMessage(x => localizer["NotValid"])
            .OverridePropertyName("DirectoryId");
        
        RuleFor(p => p.model.ExecutorId)
            .NotNull().WithMessage(x => localizer["NotNull"])
            .GreaterThan(0).WithMessage(x => localizer["NotValid"])
            .OverridePropertyName("ExecutorId");
    }
}
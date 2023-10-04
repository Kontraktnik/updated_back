using Application.Resource;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.Features.SurveyCQRS.Command.AddSurvey;

public class AddSurveyCommandValidator : AbstractValidator<AddSurveyCommand>
{
    public AddSurveyCommandValidator(IStringLocalizer<Localize> localizer)
    {
        RuleFor(r => r.model.IIN)
            .NotEmpty().WithMessage(x => localizer["Required"])
            .Must(u=>u.Length == 12).WithMessage(x => localizer["IIN_Number"])
            .Matches(@"^[0-9]*$").WithMessage(x => localizer["IIN_Number"])
            .OverridePropertyName("IIN");
        
        RuleFor(r => r.model.Phone)
            .NotEmpty().WithMessage(x => localizer["Required"])
            .Matches(@"^\+?77([0124567][0-8]\d{7})$").WithMessage(x => localizer["Phone_Rules"])
            .OverridePropertyName("Phone");
        
        RuleFor(r => r.model.Email)
            .NotEmpty().WithMessage(x => localizer["Required"])
            .Matches(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$").WithMessage(x => localizer["Email_Rules"])
            .OverridePropertyName("Email");
        
        RuleFor(p => p.model.Agreed)
            .Must(p => p==true).WithMessage(x => localizer["Required"])
            .OverridePropertyName("Agreed");
        
        RuleFor(p => p.model.BirthAreaId)
            .GreaterThan(0).WithMessage(x => localizer["NotValid"])
            .NotEmpty().WithMessage(x => localizer["NotNull"])
            .OverridePropertyName("BirthAreaId");
        
        RuleFor(p => p.model.Region)
            .NotEmpty().WithMessage(x => localizer["Required"])
            .OverridePropertyName("Region");
        
        RuleFor(p => p.model.City)
            .NotEmpty().WithMessage(x => localizer["NotNull"])
            .OverridePropertyName("City");
        
        RuleFor(p => p.model.Street)
            .NotEmpty().WithMessage("Please fill the field")
            .OverridePropertyName("Street");
        
        RuleFor(p => p.model.EducationId)
            .GreaterThan(0).WithMessage(x => localizer["NotValid"])
            .NotEmpty().WithMessage(x => localizer["NotNull"])
            .GreaterThan(0).WithMessage(x => localizer["NotValid"])
            .OverridePropertyName("EducationId");
        
        RuleFor(p => p.model.Experienced)
            .NotNull().WithMessage(x => localizer["NotNull"])
            .OverridePropertyName("Experienced");
        
        RuleFor(p => p.model.Served)
            .NotNull().WithMessage(x => localizer["NotNull"])
            .OverridePropertyName("Served");
        
        RuleFor(p => p.model.VTShServed)
            .NotNull().WithMessage(x => localizer["NotNull"])
            .OverridePropertyName("VTShServed");
        
        RuleFor(p => p.model.PositionId)
            .GreaterThan(0).WithMessage(x => localizer["NotValid"])
            .NotNull().WithMessage(x => localizer["NotNull"])
            .OverridePropertyName("PositionId");
        
        RuleFor(p => p.model.ContractYear)
            .GreaterThan(0).WithMessage(x => localizer["NotValid"])
            .NotNull().WithMessage(x => localizer["NotNull"])
            .OverridePropertyName("ContractYear");
        
        RuleFor(p => p.model.AreaId)
            .GreaterThan(0).WithMessage(x => localizer["NotValid"])
            .NotNull().WithMessage(x => localizer["NotNull"])
            .OverridePropertyName("AreaId");
        
        RuleFor(p => p.model.ImageUrl)
            .NotNull().WithMessage(x => localizer["NotNull"])
            .OverridePropertyName("ImageUrl");
        
        RuleFor(p => p.model.AutoBiography)
            .NotNull().WithMessage(x => localizer["NotNull"])
            .OverridePropertyName("AutoBiography");
        
        RuleFor(p => p.model.IncomePropertyUrl)
            .NotNull().WithMessage(x => localizer["NotNull"])
            .OverridePropertyName("IncomePropertyUrl");
        
        RuleFor(p => p.model.EmploymentUrl)
            .NotNull().WithMessage(x => localizer["NotNull"])
            .OverridePropertyName("EmploymentUrl");
        
        RuleFor(p => p.model.MillitaryUrl)
            .NotNull().WithMessage(x => localizer["NotNull"])
            .OverridePropertyName("MillitaryUrl");
        
        RuleFor(p => p.model.SpecialCheckUrl)
            .NotNull().WithMessage(x => localizer["NotNull"])
            .OverridePropertyName("SpecialCheckUrl");
        
        RuleFor(p => p.model.IdentityCardUrl)
            .NotNull().WithMessage(x => localizer["NotNull"])
            .OverridePropertyName("IdentityCardUrl");
        
        RuleFor(p => p.model.SignKey)
            .NotNull().WithMessage(x => localizer["NotNull"])
            .OverridePropertyName("IdentityCardUrl");
        
        RuleForEach(p => p.model.Relatives)
            .Must(p=>p.IIN.Length == 12).WithMessage(x => localizer["IIN_Number"])
            .When(x => x!=null)
            .NotNull().WithMessage(x => localizer["Required"])
            .OverridePropertyName("Relative_IIN");
        RuleForEach(p => p.model.Relatives)
            .Must(p=>p.Name != null).WithMessage(x => localizer["NotNull"])
            .When(x => x!=null)
            .NotNull().WithMessage(x => localizer["NotNull"])
            .OverridePropertyName("Relative_Name");
        RuleForEach(p => p.model.Relatives)
            .Must(p=>p.SurName != null).WithMessage(x => localizer["NotNull"])
            .When(x => x!=null)
            .NotNull().WithMessage(x => localizer["NotNull"])
            .OverridePropertyName("Relative_Surname");
    }
}
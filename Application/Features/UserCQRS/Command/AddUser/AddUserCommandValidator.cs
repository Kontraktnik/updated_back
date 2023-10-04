using System.ComponentModel.DataAnnotations;
using Application.Resource;
using FluentValidation;
using Microsoft.Extensions.Localization;
using static Application.Helpers.ValidationHelpers;

namespace Application.Features.UserCQRS.Command.AddUser;

public class AddUserCommandValidator : AbstractValidator<AddUserCommand>
{
    public AddUserCommandValidator(IStringLocalizer<Localize> localizer)
    {
        RuleFor(r => r.model.IIN)
            .Must(u=>u.Length == 12).WithMessage(x => localizer["IIN_Number"])
                .Matches(@"^[0-9]*$").WithMessage(x => localizer["IIN_Number"])
                .OverridePropertyName("IIN");

        RuleFor(r => r.model)
            .Must(p =>
                { 
                    if (RequiredAreaByRoles.Contains(p.RoleId)) { return p.AreaId != null; }
                    else { return p.AreaId == null; }
                }
            ).WithMessage(x => localizer["Required"])
            .OverridePropertyName("AreaId");
        
        RuleFor(r => r.model.Password)
            .NotEmpty().WithMessage("Password required")
            .Matches(@"^(?=.*[0-9]+.*)(?=.*[a-zA-Z]+.*)[0-9a-zA-Z@*_.]{6,}$").WithMessage(x => localizer["Password_Rules"])
            .OverridePropertyName("Password");
        RuleFor(r => r.model.Name)
            .NotEmpty().WithMessage(x => localizer["NotEmpty"])
            .Matches(@"[а-яА-ЯёЁәӘіІңҢғҒүҮұҰқҚөӨһҺ]+").WithMessage(x => localizer["Name_Rules"])
            .OverridePropertyName("Name");
        RuleFor(r => r.model.Surname)
            .NotEmpty().WithMessage(x => localizer["NotEmpty"])
            .Matches(@"[а-яА-ЯёЁәӘіІңҢғҒүҮұҰқҚөӨһҺ]+").WithMessage(x => localizer["Name_Rules"])
            .OverridePropertyName("Surname");
        RuleFor(r => r.model.Phone)
            .NotEmpty().WithMessage("Phone is required")
            .Matches(@"^\+?77([0124567][0-8]\d{7})$").WithMessage(x => localizer["Phone_Rules"])
            .OverridePropertyName("Phone");
        RuleFor(r => r.model.Email)
            .NotEmpty().WithMessage(x => localizer["Required"])
            .Matches(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$").WithMessage(x => localizer["Email_Rules"])
            .OverridePropertyName("Email");
    }
}
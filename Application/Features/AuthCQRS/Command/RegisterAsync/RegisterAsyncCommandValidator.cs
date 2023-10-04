using System.Text.RegularExpressions;
using Application.Resource;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace Application.Features.AuthCQRS.Command.RegisterAsync;

public class RegisterAsyncCommandValidator : AbstractValidator<RegisterAsyncCommand>
{
    public RegisterAsyncCommandValidator(IStringLocalizer<Localize> localizer)
    {
        RuleFor(r => r.RegisterData.IIN)
            .NotEmpty().WithMessage(x => localizer["Required"])
            .Must(u=>u.Length == 12).WithMessage(x => localizer["IIN_Number"])
            .Matches(@"^[0-9]*$").WithMessage(x => localizer["IIN_Number"])
            .OverridePropertyName("IIN");
        RuleFor(r => r.RegisterData.Password)
            .NotEmpty().WithMessage(x => localizer["Required"])
            .Matches(@"^(?=.*[0-9]+.*)(?=.*[a-zA-Z]+.*)[0-9a-zA-Z@*_.]{6,}$").WithMessage(x => localizer["Password_Rules"])
            .OverridePropertyName("Password");
        RuleFor(r => r.RegisterData.Name)
            .NotEmpty().WithMessage(x => localizer["Required"])
            .Matches(@"[а-яА-ЯёЁәӘіІңҢғҒүҮұҰқҚөӨһҺ]+").WithMessage(x => localizer["Name_Rules"])
            .OverridePropertyName("Name");
        RuleFor(r => r.RegisterData.Surname)
            .NotEmpty().WithMessage(x => localizer["Required"])
            .Matches(@"[а-яА-ЯёЁәӘіІңҢғҒүҮұҰқҚөӨһҺ]+").WithMessage(x => localizer["Name_Rules"])
            .OverridePropertyName("Surname");
        RuleFor(r => r.RegisterData.Phone)
            .NotEmpty().WithMessage(x => localizer["Required"])
            .Matches(@"^\+?77([0124567][0-8]\d{7})$").WithMessage(x => localizer["Phone_Rules"])
            .OverridePropertyName("Phone");
        RuleFor(r => r.RegisterData.Email)
            .NotEmpty().WithMessage(x => localizer["Required"])
            .Matches(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$").WithMessage(x => localizer["Email_Rules"])
            .OverridePropertyName("Email");
        
    }
}
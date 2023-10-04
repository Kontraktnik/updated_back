using Application.DTO.Auth;
using MediatR;

namespace Application.Features.AuthCQRS.Command.RegisterAsync;

public class RegisterAsyncCommand : IRequest<AuthResponse<bool>>
{
    public RegisterDTO RegisterData { get; set; }

    public RegisterAsyncCommand(RegisterDTO RegisterData)
    {
        this.RegisterData = RegisterData;
    }
}
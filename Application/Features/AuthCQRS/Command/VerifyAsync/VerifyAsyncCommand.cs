using Application.DTO.Auth;
using MediatR;

namespace Application.Features.AuthCQRS.Command.VerifyAsync;

public class VerifyAsyncCommand : IRequest<AuthResponse<TokenDTO>>
{
    public VerifyRegistrationDTO verifyRegistrationDto { get; set; }

    public VerifyAsyncCommand(VerifyRegistrationDTO verifyRegistrationDto)
    {
        this.verifyRegistrationDto = verifyRegistrationDto;
    }
    
}
using Application.DTO.Auth;
using MediatR;

namespace Application.Features.AuthCQRS.Query.LoginAsync;

public class LoginAsyncQuery : IRequest<AuthResponse<TokenDTO>>
{
    public LoginDTO credentials { get; set; }

    public LoginAsyncQuery(LoginDTO credentials)
    {
        this.credentials = credentials;
    }
    
}
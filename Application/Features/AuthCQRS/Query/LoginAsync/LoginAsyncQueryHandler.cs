using Application.Contracts.Persistence;
using Application.DTO.Auth;
using MediatR;

namespace Application.Features.AuthCQRS.Query.LoginAsync;

public class LoginAsyncQueryHandler : IRequestHandler<LoginAsyncQuery,AuthResponse<TokenDTO>>
{
    private readonly IAuthRepository _authRepository;

    public LoginAsyncQueryHandler(IAuthRepository authRepository)
    {
        _authRepository = authRepository;
    }
    
    
    public async Task<AuthResponse<TokenDTO>> Handle(LoginAsyncQuery request, CancellationToken cancellationToken)
    {
        return await _authRepository.LoginAsync(request.credentials);
    }
}
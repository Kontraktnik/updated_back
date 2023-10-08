using Application.Contracts.Persistence;
using Application.DTO.Auth;
using MediatR;

namespace Application.Features.AuthCQRS.Query.EcpLoginAsync;

public class EcpLoginAsyncQueryHandler : IRequestHandler<EcpLoginAsyncQuery,AuthResponse<TokenDTO>>
{
    private readonly IAuthRepository _authRepository;

    public EcpLoginAsyncQueryHandler(IAuthRepository authRepository)
    {
        _authRepository = authRepository;
    }
    
    
    public async Task<AuthResponse<TokenDTO>> Handle(EcpLoginAsyncQuery request, CancellationToken cancellationToken)
    {
        return await _authRepository.EcpLoginAsync(request.credentials);
    }
}
using Application.DTO.Auth;
using MediatR;

namespace Application.Features.AuthCQRS.Query.EcpLoginAsync;

public class EcpLoginAsyncQuery : IRequest<AuthResponse<TokenDTO>>
{
    public EcpLoginDTO credentials { get; set; }

    public EcpLoginAsyncQuery(EcpLoginDTO credentials)
    {
        this.credentials = credentials;
    }
    
}
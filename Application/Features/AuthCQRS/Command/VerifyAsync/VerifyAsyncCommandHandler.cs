using Application.Contracts.Persistence;
using Application.DTO.Auth;
using AutoMapper;
using MediatR;

namespace Application.Features.AuthCQRS.Command.VerifyAsync;

public class VerifyAsyncCommandHandler : IRequestHandler<VerifyAsyncCommand,AuthResponse<TokenDTO>>
{
    private IAuthRepository _authRepository;
    private IMapper _mapper;

    public VerifyAsyncCommandHandler(IAuthRepository authRepository,IMapper mapper)
    {
        _authRepository = authRepository;
        _mapper = mapper;
    }
    
    
    public async Task<AuthResponse<TokenDTO>> Handle(VerifyAsyncCommand request, CancellationToken cancellationToken)
    {
        return await _authRepository.VerifyAsync(request.verifyRegistrationDto);
    }
}
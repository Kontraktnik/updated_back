using Application.Contracts.Persistence;
using Application.DTO.Auth;
using AutoMapper;
using Domain.Models.UserModels;
using MediatR;

namespace Application.Features.AuthCQRS.Command.RegisterAsync;

public class RegisterAsyncCommandHandler : IRequestHandler<RegisterAsyncCommand,AuthResponse<bool>>
{
    private IAuthRepository _authRepository;
    private IMapper _mapper;

    public RegisterAsyncCommandHandler(IAuthRepository authRepository,IMapper mapper)
    {
        _authRepository = authRepository;
        _mapper = mapper;
    }

    public async Task<AuthResponse<bool>> Handle(RegisterAsyncCommand request, CancellationToken cancellationToken)
    {
        User user = _mapper.Map<User>(request.RegisterData);
        
        return  await _authRepository.RegisterAsync(user);
    }
}
using Application.DTO.Auth;
using Domain.Models.UserModels;

namespace Application.Contracts.Persistence;

public interface IAuthRepository
{
    public Task<AuthResponse<bool>> RegisterAsync(User user);
    public Task<AuthResponse<TokenDTO>> LoginAsync(LoginDTO loginDto);

    public Task<AuthResponse<TokenDTO>> VerifyAsync(VerifyRegistrationDTO verifyRegistrationDto);

}
using Application.DTO.User;

namespace Application.DTO.Auth;

public class TokenDTO
{
    public string jwtToken { get; set; }
    public int Expires { get; set; }
    public UserDTO UserDto { get; set; }
}
namespace Application.DTO.Auth;

public class LoginDTO
{
  
    public string? IIN { get; set; }
    public string Password { get; set; }
    
    public string? Code { get; set; }
}

public class EcpLoginDTO
{
    public string IIN { get; set; }
    public string SignText { get; set; }
}
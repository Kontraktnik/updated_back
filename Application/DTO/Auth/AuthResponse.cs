namespace Application.DTO.Auth;

public class AuthResponse<T>
{
    public int StatusCode { get; set; }
    public string? Message { get; set; }
    public bool Success { get; set; }
    public T Data { get; set; }
    
}
namespace Application.DTO.User;

public class UserUpdateDTO
{
    public long Id { get; set; }
    public long RoleId { get; set; }
    //Area
    public long? AreaId { get; set; }
    //Common Fields
    public string? ImageUrl { get; set; }
    public string IIN { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string? Patronymic { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string? Password { get; set; }
    
    public bool Verified { get; set; }
    
    public bool Status { get; set; }
}
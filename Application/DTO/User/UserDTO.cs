using Application.DTO.System;

namespace Application.DTO.User;

public class UserDTO
{
    public long Id { get; set; }

    public long RoleId { get; set; }
    public virtual  RoleDTO Role { get; set; }
    //Area
    public long? AreaId { get; set; }
    public virtual  AreaDTO? Area { get; set; }
    //Common Fields
    public string? ImageUrl { get; set; }
    public string IIN { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string? Patronymic { get; set; }
    public string FullName { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    
    public bool Verified { get; set; }
    
    public bool Status { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
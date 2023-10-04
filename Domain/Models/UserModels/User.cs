using Domain.Models.DigitalSignModels;
using Domain.Models.SystemModels;
using System.Text.Json.Serialization;

namespace Domain.Models.UserModels;

public class User : BaseModel
{
    //Role
    public long RoleId { get; set; }
    public virtual  Role Role { get; set; }
    //Area
    public long? AreaId { get; set; }
    public virtual  Area? Area { get; set; }
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

    [JsonIgnore]
    public virtual ICollection<DigitalSign> DigitalSigns { get; set; }

}
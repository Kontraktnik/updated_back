using System.ComponentModel.DataAnnotations.Schema;
using Domain.Models.UserModels;

namespace Domain.Models.ProfileModels;

public class ProfileFile : BaseModel
{
    public long ProfileId { get; set; }
    public Profile Profile { get; set; }
    
    public string File { get; set; }
    public string? Comment { get; set; }
    
    public bool? isConfirmated { get; set; }
    public bool? isRequested { get; set; }
    
    [ForeignKey("User")]
    public long? UserId { get; set; }
    public virtual User? User { get; set; }
    
    
}
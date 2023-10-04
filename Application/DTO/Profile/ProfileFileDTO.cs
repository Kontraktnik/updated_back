using Application.DTO.User;

namespace Application.DTO.Profile;

public class ProfileFileDTO
{
    public long Id { get; set; }
    public long ProfileId { get; set; }
    public ProfileDTO Profile { get; set; }
    
    public string File { get; set; }
    public string? Comment { get; set; }
    
    public bool? isConfirmated { get; set; }
    public bool? isRequested { get; set; }
    
    public long? UserId { get; set; }
    public virtual UserDTO? User { get; set; }
    
    
}
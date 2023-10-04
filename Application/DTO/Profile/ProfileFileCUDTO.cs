namespace Application.DTO.Profile;

public class ProfileFileCUDTO
{
    public long ProfileId { get; set; }
    public string? File { get; set; }
    public string? Comment { get; set; }
    
    public bool? isConfirmated { get; set; }
    public bool? isRequested { get; set; }
    public long? UserId { get; set; }
}
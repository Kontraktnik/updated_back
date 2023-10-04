namespace Application.DTO.Profile;

public class SendConfirmationDTO
{
    public string? Comment { get; set; }
    public string SignKey { get; set; }
    public int Status { get; set; }
    
    public long? MedicalStatusId { get; set; }
}
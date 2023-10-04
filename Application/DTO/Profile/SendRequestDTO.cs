namespace Application.DTO.Profile;

public class SendRequestDTO
{
    public long StepId { get; set; }
    public long SurveyId { get; set; }
    public string? SignKey { get; set; }
    public int Status { get; set; }
}
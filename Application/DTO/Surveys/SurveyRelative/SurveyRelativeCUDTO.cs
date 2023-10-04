namespace Application.DTO.Survey;

public class SurveyRelativeCUDTO
{
    public long RelativeId { get; set; }
    
    public long SurveyId { get; set; }
    
    public string Name { get; set; }
    public string SurName { get; set; }
    public string? Patronomic { get; set; }
    public string IIN { get; set; }
    public string BirthDate { get; set; }
}
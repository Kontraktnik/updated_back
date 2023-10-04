using Application.DTO.System;

namespace Application.DTO.Survey;

public class SurveyRelativeDTO
{
    public long RelativeId { get; set; }
    public virtual RelativeDTO Relative { get; set; }
    
    public long SurveyId { get; set; }
    public virtual SurveyDTO Survey { get; set; }
    
    public string Name { get; set; }
    public string SurName { get; set; }
    public string? Patronomic { get; set; }
    public string IIN { get; set; }
    public string BirthDate { get; set; }
}
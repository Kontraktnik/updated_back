using Domain.Models.SystemModels;

namespace Domain.Models.SurveyModels;

public class SurveyRelative : BaseModel
{
    public long RelativeId { get; set; }
    public virtual Relative Relative { get; set; }
    
    public long SurveyId { get; set; }
    public virtual Survey Survey { get; set; }
    
    public string Name { get; set; }
    public string SurName { get; set; }
    public string? Patronomic { get; set; }
    public string IIN { get; set; }
    public string BirthDate { get; set; }

    
}
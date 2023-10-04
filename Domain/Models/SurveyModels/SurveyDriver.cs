using Domain.Models.SystemModels;

namespace Domain.Models.SurveyModels;

public class SurveyDriver : BaseModel
{
    
    public long SurveyId { get; set; }
    public virtual Survey Survey { get; set; }
    
    public long DriverLicenseId { get; set; }
    public virtual DriverLicense DriverLicense { get; set; }
    
}
using Application.DTO.System;

namespace Application.DTO.Survey;

public class SurveyDriverDTO
{
    public long SurveyId { get; set; }
    public virtual SurveyDTO Survey { get; set; }
    
    public long DriverLicenseId { get; set; }
    public virtual DriverLicenseDTO DriverLicense { get; set; }
}
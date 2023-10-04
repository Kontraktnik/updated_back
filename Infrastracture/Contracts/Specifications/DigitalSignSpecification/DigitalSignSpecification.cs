using Domain.Models.DigitalSignModels;

namespace Infrastracture.Contracts.Specifications.DigitalSignSpecification;

public class DigitalSignSpecification : BaseSpecification<DigitalSign>
{
    public DigitalSignSpecification()
    {
        AddInclude("SignedUser");
    }

    public DigitalSignSpecification(long surveyId) : base(p => p.SurveyId == surveyId)
    {
        AddInclude("SignedUser");
        AddInclude("Info");
    }
}
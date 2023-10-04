using Domain.Models.SurveyModels;

namespace Infrastracture.Contracts.Specifications.SurveySpecification;

public class SurveyMailSpecification : BaseSpecification<Survey>
{
    public SurveyMailSpecification(long Id) : base(p=>p.Id.Equals(Id))
    {
        AddInclude("StepGroup");
        AddInclude("CurrentStep");
        AddInclude("User");
    }
}
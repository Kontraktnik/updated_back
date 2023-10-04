using Domain.Models.StepModels;

namespace Infrastracture.Contracts.Specifications.StepSpecification;

public class StepSpecification : BaseSpecification<Step>
{
    public StepSpecification()
    {
        AddInclude("StepGroup");
    }
    
    public StepSpecification(long Id) : base(p => p.Id.Equals(Id))
    {
        AddInclude("StepGroup");

    }
}
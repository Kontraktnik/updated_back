using Application.Contracts.Persistence;
using Domain.Models.StepModels;

namespace Infrastracture.Contracts.Specifications.StepOrderSpecification;

public class StepOrderSpecification : BaseSpecification<StepOrder>
{
    public StepOrderSpecification()
    {
        
    }

    public StepOrderSpecification(long StepId) : base(p=>p.StepId == StepId)
    {
        
    }
}
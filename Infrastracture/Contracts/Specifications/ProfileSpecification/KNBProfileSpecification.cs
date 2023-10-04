using Domain.Models.ProfileModels;
using Infrastracture.Contracts.Parameters.ProfileParameters;

namespace Infrastracture.Contracts.Specifications.ProfileSpecification;

public class KNBProfileSpecification : BaseSpecification<Profile>
{
    public KNBProfileSpecification(KNBProfileParameters parameters)
        : base(
            p=> (!parameters.StepId.HasValue || p.StepId.Equals(parameters.StepId)) 
                && (!parameters.RequestedStatus.HasValue || p.RequestedStatus.Equals(parameters.RequestedStatus))
                && (!parameters.ConfirmedUserId.HasValue || p.ConfirmedUserId.Equals(parameters.ConfirmedUserId))
                && p.Status.Equals(parameters.Status)
                && p.AreaId.Equals(parameters.AreaId)
        )
    {
        AddInclude("StepGroup");
        AddInclude("Step");
        AddInclude("Survey.User");
        AddInclude("Survey.CurrentStep");
        AddOrderByDescending(p=> p.UpdatedAt);
        ApplyPaging(parameters.PageSize * (parameters.PageIndex - 1), parameters.PageSize);

        
    }
}

public class KNBProfileCountSpecification : BaseSpecification<Profile>
{
    public KNBProfileCountSpecification(KNBProfileParameters parameters)
        : base(
            p=> (!parameters.StepId.HasValue || p.StepId.Equals(parameters.StepId)) 
                && (!parameters.ConfirmedUserId.HasValue || p.ConfirmedUserId.Equals(parameters.ConfirmedUserId))
                && (!parameters.RequestedStatus.HasValue || p.RequestedStatus.Equals(parameters.RequestedStatus))
                && p.Status.Equals(parameters.Status)
                && p.AreaId.Equals(parameters.AreaId)
        )
    {
        
    }
}
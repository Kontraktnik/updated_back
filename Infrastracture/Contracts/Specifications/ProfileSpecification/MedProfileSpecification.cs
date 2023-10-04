using Domain.Models.ProfileModels;
using Infrastracture.Contracts.Parameters.ProfileParameters;

namespace Infrastracture.Contracts.Specifications.ProfileSpecification;

public class MedProfileSpecification : BaseSpecification<Profile>
{
    public MedProfileSpecification(MedProfileParameters parameters)
        : base(
            p=> parameters.StepId.Contains(p.StepId)
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

public class MedProfileCountSpecification : BaseSpecification<Profile>
{
    public MedProfileCountSpecification(MedProfileParameters parameters)
        : base(
            p=> parameters.StepId.Contains(p.StepId)
                && (!parameters.ConfirmedUserId.HasValue || p.ConfirmedUserId.Equals(parameters.ConfirmedUserId))
                && p.Status.Equals(parameters.Status)
                && p.AreaId.Equals(parameters.AreaId)
        )
    {
        
    }
}
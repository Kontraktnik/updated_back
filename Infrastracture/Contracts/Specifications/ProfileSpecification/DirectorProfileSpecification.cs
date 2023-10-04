using Domain.Models.ProfileModels;
using Infrastracture.Contracts.Parameters.ProfileParameters;

namespace Infrastracture.Contracts.Specifications.ProfileSpecification;

public class DirectorProfileSpecification : BaseSpecification<Profile>
{
    public DirectorProfileSpecification(DirectorProfileParameters parameters,bool isPaging = true)
    : base(
        p=> (!parameters.StepId.HasValue || p.StepId.Equals(parameters.StepId)) 
              && (!parameters.ConfirmedUserId.HasValue || p.ConfirmedUserId.Equals(parameters.ConfirmedUserId))
              && p.Status.Equals(parameters.Status)
              //&& p.AreaId.Equals(parameters.AreaId)
              )
    {
        if (isPaging)
        {
            AddInclude("StepGroup");
            AddInclude("Step");
            AddInclude("Survey.User");
            AddInclude("Survey.CurrentStep");
            AddOrderByDescending(p=> p.CreatedAt);
            ApplyPaging(parameters.PageSize * (parameters.PageIndex - 1), parameters.PageSize);
        }


        
    }
}

public class DirectorProfileCountSpecification : BaseSpecification<Profile>
{
    public DirectorProfileCountSpecification(DirectorProfileParameters parameters)
        : base(
            p=> (!parameters.StepId.HasValue || p.StepId.Equals(parameters.StepId)) 
                && (!parameters.ConfirmedUserId.HasValue || p.ConfirmedUserId.Equals(parameters.ConfirmedUserId))
                && p.Status.Equals(parameters.Status)
                //&& p.AreaId.Equals(parameters.AreaId)
        )
    {
        
        
    }
}
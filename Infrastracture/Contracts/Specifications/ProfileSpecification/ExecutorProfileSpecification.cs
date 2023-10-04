using Domain.Models.ProfileModels;
using Infrastracture.Contracts.Parameters.ProfileParameters;

namespace Infrastracture.Contracts.Specifications.ProfileSpecification;

public class ExecutorProfileSpecification : BaseSpecification<Profile>
{
    public ExecutorProfileSpecification(ExecutorProfileParameter parameters,bool isPaging = true)
        : base(
            p=> (!parameters.StepId.HasValue || p.StepId.Equals(parameters.StepId)) 
                && (!parameters.ConfirmedUserId.HasValue || p.ConfirmedUserId.Equals(parameters.ConfirmedUserId))
                && (!parameters.RequestedUserId.HasValue || p.RequestedUserId.Equals(parameters.RequestedUserId))
                && parameters.Status.Contains(p.Status)
                && (parameters.RequestedStatus == null || parameters.RequestedStatus.Count == 0 || parameters.RequestedStatus.Contains(p.RequestedStatus))
                && p.AreaId.Equals(parameters.AreaId)
        )
    {
        AddInclude("StepGroup");
        AddInclude("Step");
        AddInclude("Survey.User");
        AddInclude("Survey.CurrentStep");
        AddOrderByDescending(p=> p.UpdatedAt);
        if (isPaging)
        {
            ApplyPaging(parameters.PageSize * (parameters.PageIndex - 1), parameters.PageSize);
        }
        
    }
    
    public ExecutorProfileSpecification(List<long> Surveys,int Status,int pageSize,int pageIndex,bool isPaging = true)
        : base(
            p=> Surveys.Contains(p.SurveyId) && p.Status.Equals(Status)
        )
    {
        AddInclude("StepGroup");
        AddInclude("Step");
        AddInclude("Survey.User");
        if (isPaging)
        {
            ApplyPaging(pageSize * (pageIndex - 1), pageSize);
        }
        
    }
}
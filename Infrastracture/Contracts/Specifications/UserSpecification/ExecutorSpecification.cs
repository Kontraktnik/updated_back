using Domain.Models.UserModels;
using Infrastracture.Helpers;

namespace Infrastracture.Contracts.Specifications.UserSpecification;

public class ExecutorSpecification : BaseSpecification<User>
{
    public ExecutorSpecification() : base(p=>p.Status.Equals(true) && p.RoleId.Equals(AppConstant.ExecutorRoleId) && p.Verified.Equals(true))
    {
        AddInclude("Role");
        AddInclude("Area");
    }
    public ExecutorSpecification(long AreaId) : base(p=>p.AreaId.Equals(AreaId) && p.Status.Equals(true) && p.RoleId.Equals(AppConstant.ExecutorRoleId) && p.Verified.Equals(true))
    {
        AddInclude("Role");
        AddInclude("Area");
    }
}
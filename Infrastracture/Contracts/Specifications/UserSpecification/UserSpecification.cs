using Domain.Models.UserModels;
using Infrastracture.Contracts.Parameters.UserParameters;

namespace Infrastracture.Contracts.Specifications.UserSpecification;

public class UserSpecification : BaseSpecification<User>
{

    public UserSpecification()
    {
        AddInclude("Role");
        AddInclude("Area");
    }

    public UserSpecification(UserParameter parameter) :
        base(a=>
            //Parameters
            (
                //Search Parameters
                (string.IsNullOrEmpty(parameter.Search) || 
                 a.FullName.ToLower().Contains(parameter.Search.ToLower().Trim()) ||
                 a.IIN.ToLower().Contains(parameter.Search.ToLower().Trim()) ||
                 a.Email.ToLower().Contains(parameter.Search.ToLower().Trim()) ||
                 a.Phone.ToLower().Contains(parameter.Search.ToLower().Trim()))
                &&//Verified 
                (!parameter.Verified.HasValue || a.Verified == parameter.Verified)
                &&//Status not banned banned
                (!parameter.Status.HasValue || a.Status == parameter.Status)
                &&//Role 
                (!parameter.RoleId.HasValue || a.RoleId.Equals(parameter.RoleId))
                &&//Area 
                (!parameter.AreaId.HasValue || a.AreaId.Equals(parameter.AreaId))
            )
        )
    {
        AddInclude("Role");
        AddInclude("Area");
        ApplyPaging(parameter.PageSize * (parameter.PageIndex - 1), parameter.PageSize);
    }
    
    
    public UserSpecification(string IIN,string? defaultValue = null) :
        base(p=>p.IIN.Equals(IIN))
    {
        AddInclude("Role");
        AddInclude("Area");
    }
    
}

public class CountUserSpecification : BaseSpecification<User>
{
    public CountUserSpecification()
    {
        
    }

    public CountUserSpecification(UserParameter parameter) :
        base(a=>
            //Parameters
            (
                //Search Parameters
                (string.IsNullOrEmpty(parameter.Search) || 
                 a.FullName.ToLower().Contains(parameter.Search.ToLower().Trim()) ||
                 a.IIN.ToLower().Contains(parameter.Search.ToLower().Trim()) ||
                 a.Email.ToLower().Contains(parameter.Search.ToLower().Trim()) ||
                 a.Phone.ToLower().Contains(parameter.Search.ToLower().Trim()))
                &&//Verified 
                (!parameter.Verified.HasValue || a.Verified == parameter.Verified)
                &&//Status not banned banned
                (!parameter.Status.HasValue || a.Status == parameter.Status)
                &&//Role 
                (!parameter.RoleId.HasValue || a.RoleId.Equals(parameter.RoleId))
                &&//Area 
                (!parameter.AreaId.HasValue || a.AreaId.Equals(parameter.AreaId))
            )
        )
    {
    }
}
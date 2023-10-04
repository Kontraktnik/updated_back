using Domain.Models.SystemModels;
using Infrastracture.Contracts.Parameters.ArmyDepartmentParameters;

namespace Infrastracture.Contracts.Specifications.ArmyDepartmentSpecification;

public class ArmyDepartmentSpecification : BaseSpecification<ArmyDepartment>
{
    public ArmyDepartmentSpecification()
    {
        
    }

    public ArmyDepartmentSpecification(ArmyDepartmentParameter parameter):
        base(a=>
            (string.IsNullOrEmpty(parameter.Search) || 
             a.TitleEn.ToLower().Contains(parameter.Search.ToLower()) ||
             a.TitleRu.ToLower().Contains(parameter.Search.ToLower()) ||
             a.TitleKz.ToLower().Contains(parameter.Search.ToLower()))
        )
    {
        ApplyPaging(parameter.PageSize * (parameter.PageIndex - 1), parameter.PageSize);
    }
}
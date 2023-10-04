using Domain.Models.SystemModels;
using Infrastracture.Contracts.Parameters.ArmyRegionParameters;

namespace Infrastracture.Contracts.Specifications.ArmyRegionSpecification;

public class ArmyRegionSpecification : BaseSpecification<ArmyRegion>
{
    public ArmyRegionSpecification()
    {
        
    }
    
    public ArmyRegionSpecification(ArmyRegionParameter parameter):
        base(a=>
            (string.IsNullOrEmpty(parameter.Search) || 
             a.TitleEn.ToLower().Contains(parameter.Search.ToLower()) ||
             a.TitleRu.ToLower().Contains(parameter.Search.ToLower()) ||
             a.TitleKz.ToLower().Contains(parameter.Search.ToLower()))
        )
    {
        //ApplyPaging(parameter.PageSize * (parameter.PageIndex - 1), parameter.PageSize);
    }
}
using Domain.Models.SystemModels;
using Infrastracture.Contracts.Parameters.ArmyTypeParameters;

namespace Infrastracture.Contracts.Specifications.ArmyTypeSpecification;

public class ArmyTypeSpecification : BaseSpecification<ArmyType>
{
    public ArmyTypeSpecification()
    {
        
    }
    
    public ArmyTypeSpecification(ArmyTypeParameter parameter):
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
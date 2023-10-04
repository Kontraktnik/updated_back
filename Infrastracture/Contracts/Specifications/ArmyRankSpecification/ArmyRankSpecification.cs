using Domain.Models.SystemModels;
using Infrastracture.Contracts.Parameters.ArmyRankParameters;

namespace Infrastracture.Contracts.Specifications.ArmyRankSpecification;

public class ArmyRankSpecification : BaseSpecification<ArmyRank>
{
    public ArmyRankSpecification()
    {
        
    }
    
    public ArmyRankSpecification(ArmyRankParameter parameter):
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
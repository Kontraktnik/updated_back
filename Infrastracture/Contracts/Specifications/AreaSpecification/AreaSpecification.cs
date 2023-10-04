using Domain.Models.SystemModels;
using Infrastracture.Contracts.Parameters.AreaParameters;

namespace Infrastracture.Contracts.Specifications.AreaSpecification;

public class AreaSpecification : BaseSpecification<Area>
{
    public AreaSpecification()
    {
        
    }

    public AreaSpecification(AreaParameter parameter):
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
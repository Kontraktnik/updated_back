using Domain.Models.SystemModels;
using Infrastracture.Contracts.Parameters.VTShParameters;

namespace Infrastracture.Contracts.Specifications.VTShSpecification;

public class VTShSpecification : BaseSpecification<VTSh>
{
    public VTShSpecification()
    {
        
    }
    
    public VTShSpecification(VTShParameter parameter):
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
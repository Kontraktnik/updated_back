using Domain.Models.SystemModels;
using Infrastracture.Contracts.Parameters.EducationParameters;

namespace Infrastracture.Contracts.Specifications.EducationSpecification;

public class EducationSpecification : BaseSpecification<Education>
{
    public EducationSpecification()
    {
        
    }
    
    public EducationSpecification(EducationParameter parameter):
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
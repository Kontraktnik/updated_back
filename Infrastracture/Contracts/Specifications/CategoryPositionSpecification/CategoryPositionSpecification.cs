using Domain.Models.CalculationModels;
using Infrastracture.Contracts.Parameters.CategoryPositionParameters;

namespace Infrastracture.Contracts.Specifications.CategoryPositionSpecification;

public class CategoryPositionSpecification : BaseSpecification<CategoryPosition>
{
    public CategoryPositionSpecification()
    {
        
    }
    
    public CategoryPositionSpecification(CategoryPositionParameter parameter):
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
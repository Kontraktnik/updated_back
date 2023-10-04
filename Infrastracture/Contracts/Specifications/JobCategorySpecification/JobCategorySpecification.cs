using Domain.Models.CalculationModels;
using Infrastracture.Contracts.Parameters.JobCategoryParameters;

namespace Infrastracture.Contracts.Specifications.JobCategorySpecification;

public class JobCategorySpecification : BaseSpecification<JobCategory>
{
    public JobCategorySpecification()
    {
        
    }
    
    public JobCategorySpecification(JobCategoryParameter parameter):
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
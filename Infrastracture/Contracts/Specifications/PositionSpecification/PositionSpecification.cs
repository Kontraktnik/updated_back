using Domain.Models.CalculationModels;
using Domain.Models.SystemModels;
using Infrastracture.Contracts.Parameters.PositionParameters;

namespace Infrastracture.Contracts.Specifications.PositionSpecification;

public class PositionSpecification : BaseSpecification<Position>
{
    public PositionSpecification()
    {
        
    }
    
    public PositionSpecification(PositionParameter parameter,bool isPaging = true):
        base(a=>
            (string.IsNullOrEmpty(parameter.Search) || 
             a.TitleEn.ToLower().Contains(parameter.Search.ToLower()) ||
             a.TitleRu.ToLower().Contains(parameter.Search.ToLower()) ||
             a.TitleKz.ToLower().Contains(parameter.Search.ToLower())) &&
            (!parameter.SecretId.HasValue || a.SecretLevelId.Equals(parameter.SecretId)) &&
            (!parameter.CategoryPositionId.HasValue || a.CategoryPositionId.Equals(parameter.CategoryPositionId)) &&
            (!parameter.JobCategoryId.HasValue || a.JobCategoryId.Equals(parameter.JobCategoryId)) &&
            (!parameter.ArmyTypeId.HasValue || a.ArmyTypeId.Equals(parameter.ArmyTypeId))
        )
    {
        AddInclude($"{nameof(JobCategory)}");
        AddInclude($"{nameof(SecretLevel)}");
        AddInclude($"{nameof(ArmyType)}");
        AddInclude($"{nameof(CategoryPosition)}");
        if (isPaging)
        {
            ApplyPaging(parameter.PageSize * (parameter.PageIndex - 1), parameter.PageSize);
        }
    }
   
    public PositionSpecification(long Id, string? parameter) : base(p=>p.Id == Id)
    {
        AddInclude($"{nameof(JobCategory)}");
        AddInclude($"{nameof(SecretLevel)}");
        AddInclude($"{nameof(ArmyType)}");
        AddInclude($"{nameof(CategoryPosition)}");
    }
}
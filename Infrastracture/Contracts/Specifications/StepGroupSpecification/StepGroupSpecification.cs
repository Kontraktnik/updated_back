using Domain.Models.StepModels;
using Infrastracture.Contracts.Parameters.StepGroupParameters;

namespace Infrastracture.Contracts.Specifications.StepGroupSpecification;

public class StepGroupSpecification : BaseSpecification<StepGroup>
{
    public StepGroupSpecification()
    {
        AddInclude("Steps");
    }

    public StepGroupSpecification(StepGroupParameter parameter) :
        base(a =>
            (string.IsNullOrEmpty(parameter.Search) ||
             a.TitleEn.ToLower().Contains(parameter.Search.ToLower()) ||
             a.TitleRu.ToLower().Contains(parameter.Search.ToLower()) ||
             a.TitleKz.ToLower().Contains(parameter.Search.ToLower())
             )
        )
    {
        AddInclude("Steps");
        //ApplyPaging(parameter.PageSize * (parameter.PageIndex - 1), parameter.PageSize);
    }
    
    public StepGroupSpecification(long Id,string? defaultValue = null) : base(p=>p.Id == Id)
    {
        AddInclude("Steps");
    }
}
using Domain.Models.SystemModels;
using Infrastracture.Contracts.Parameters.RelativeParameters;

namespace Infrastracture.Contracts.Specifications.RelativeSpecification;

public class RelativeSpecification : BaseSpecification<Relative>
{
    public RelativeSpecification()
    {
        
    }

    public RelativeSpecification(RelativeParameter parameter) :
        base(a =>
            (string.IsNullOrEmpty(parameter.Search) ||
             a.TitleEn.ToLower().Contains(parameter.Search.ToLower()) ||
             a.TitleRu.ToLower().Contains(parameter.Search.ToLower()) ||
             a.TitleKz.ToLower().Contains(parameter.Search.ToLower()))
        )
    {
        //ApplyPaging(parameter.PageSize * (parameter.PageIndex - 1), parameter.PageSize);
    }
}
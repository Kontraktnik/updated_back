using Domain.Models.CalculationModels;
using Domain.Models.SystemModels;
using Infrastracture.Contracts.Parameters.ServiceYearParameters;

namespace Infrastracture.Contracts.Specifications.ServiceYearSpecification;

public class ServiceYearSpecification : BaseSpecification<ServiceYear>
{
    public ServiceYearSpecification()
    {

    }

    public ServiceYearSpecification(ServiceYearParameter parameter) :
        base(a =>
            (string.IsNullOrEmpty(parameter.Search) ||
             a.TitleEn.ToLower().Contains(parameter.Search.ToLower()) ||
             a.TitleRu.ToLower().Contains(parameter.Search.ToLower()) ||
             a.TitleKz.ToLower().Contains(parameter.Search.ToLower()))
            || (!parameter.Year.HasValue || (a.Max>parameter.Year && a.Min < parameter.Year ))
            )
    {
        //ApplyPaging(parameter.PageSize * (parameter.PageIndex - 1), parameter.PageSize);
    }
}
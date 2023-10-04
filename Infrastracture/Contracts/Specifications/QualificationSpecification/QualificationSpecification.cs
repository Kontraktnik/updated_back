using Domain.Models.CalculationModels;
using Infrastracture.Contracts.Parameters.QualificationParameters;

namespace Infrastracture.Contracts.Specifications.QualificationSpecification;

public class QualificationSpecification : BaseSpecification<Qualification>
{
    public QualificationSpecification()
    {

    }

    public QualificationSpecification(QualificationParameter parameter) :
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
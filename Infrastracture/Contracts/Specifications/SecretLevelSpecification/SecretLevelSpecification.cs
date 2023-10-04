using Domain.Models.CalculationModels;
using Infrastracture.Contracts.Parameters.SecretLevelParameters;

namespace Infrastracture.Contracts.Specifications.SecretLevelSpecification;

public class SecretLevelSpecification : BaseSpecification<SecretLevel>
{
    public SecretLevelSpecification()
    {

    }

    public SecretLevelSpecification(SecretLevelParameter parameter) :
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
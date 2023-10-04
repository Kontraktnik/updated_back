using Domain.Models.SystemModels;
using Infrastracture.Contracts.Parameters.DriverLicenseParameters;

namespace Infrastracture.Contracts.Specifications.DriverLicenseSpecification;

public class DriverLicenseSpecification : BaseSpecification<DriverLicense>
{
    public DriverLicenseSpecification()
    {
        
    }
    
    public DriverLicenseSpecification(DriverLicenseParameter parameter):
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
using Domain.Models.SystemModels;
using Infrastracture.Contracts.Parameters.MedicalStatusParameters;

namespace Infrastracture.Contracts.Specifications.MedicalStatusSpecification;

public class MedicalStatusSpecification : BaseSpecification<MedicalStatus>
{
    public MedicalStatusSpecification()
    {
        
    }
    public MedicalStatusSpecification(MedicalStatusParameter parameter) :
        base(a =>
            (string.IsNullOrEmpty(parameter.Search) ||
             a.TitleEn.ToLower().Contains(parameter.Search.ToLower()) ||
             a.TitleRu.ToLower().Contains(parameter.Search.ToLower()) ||
             a.TitleKz.ToLower().Contains(parameter.Search.ToLower()))
        )
    {
        //ApplyPaging(parameter.PageSize * (parameter.PageIndex - 1), parameter.PageSize);
    }
    
    public MedicalStatusSpecification(string Code, string? nullable = null) : base(p=>p.Code.Equals(Code))
    {
        
    }
    
}
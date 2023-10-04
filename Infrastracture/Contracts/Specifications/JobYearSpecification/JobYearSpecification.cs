using Domain.Models.CalculationModels;
using Infrastracture.Contracts.Parameters.JobYearParameters;

namespace Infrastracture.Contracts.Specifications.JobYearSpecification;

public class JobYearSpecification : BaseSpecification<JobYear>
{
    public JobYearSpecification(long Id) : base(p=>p.Id.Equals(Id))
    {
        AddInclude($"{nameof(JobCategory)}");
        AddInclude($"{nameof(ServiceYear)}");

    }
    
    public JobYearSpecification(JobYearParameter parameter,bool isPaging = true)
    :base(p=>
        (!parameter.JobCategoryId.HasValue || p.JobCategoryId.Equals(parameter.JobCategoryId))&&
        (!parameter.ServiceYearId.HasValue || p.ServiceYearId.Equals(parameter.ServiceYearId)))
    {
        AddInclude($"{nameof(JobCategory)}");
        AddInclude($"{nameof(ServiceYear)}");
        if (isPaging)
        {
            ApplyPaging(parameter.PageSize * (parameter.PageIndex - 1), parameter.PageSize);

        }

    }
}
using Domain.Models.CalculationModels;
using Domain.Models.SystemModels;
using Infrastracture.Contracts.Parameters.RankSalaryParameters;

namespace Infrastracture.Contracts.Specifications.RankSalarySpecification;

public class RankSalarySpecification : BaseSpecification<RankSalary>
{
    public RankSalarySpecification()
    {
        AddInclude($"{nameof(ArmyRank)}");

    }
    
   public RankSalarySpecification(RankSalaryParameter parameter,bool isPaging = true) : base(p=>(!parameter.RankId.HasValue ||p.ArmyRankId == parameter.RankId))
    {
        AddInclude($"{nameof(ArmyRank)}");
        if (isPaging)
        {
            ApplyPaging(parameter.PageSize * (parameter.PageIndex - 1), parameter.PageSize);
        }

    }
   
   public RankSalarySpecification(long Id, string? parameter) : base(p=>p.Id == Id)
   {
       AddInclude($"{nameof(ArmyRank)}");
   }
}
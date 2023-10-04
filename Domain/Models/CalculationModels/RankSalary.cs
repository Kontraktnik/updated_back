using Domain.Models.SystemModels;

namespace Domain.Models.CalculationModels;

public class RankSalary : BaseModel
{
    public long ArmyRankId { get; set; }
    public ArmyRank ArmyRank { get; set; }
    
    public int Salary { get; set; }
}
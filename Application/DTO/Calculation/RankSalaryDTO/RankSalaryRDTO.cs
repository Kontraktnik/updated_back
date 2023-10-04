using Application.DTO.System;

namespace Application.DTO.Calculation.RankSalaryDTO;

public class RankSalaryRDTO
{
    public long Id { get; set; }
    public long ArmyRankId { get; set; }
    public ArmyRankDTO ArmyRank { get; set; }
    
    public int Salary { get; set; }
}
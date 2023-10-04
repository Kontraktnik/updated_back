using Application.DTO.Calculation.RankSalaryDTO;
using Application.DTO.Common;
using Domain.Models.CalculationModels;

namespace Application.Contracts.Persistence;

public interface IRankSalaryRepository : IGenericRepository<RankSalary>
{
    public Task<ResponseDTO<RankSalaryRDTO>> GetByRankId(long RankId);
}
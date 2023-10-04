using Application.DTO.Calculation;
using Application.DTO.Common;

namespace Application.Contracts.Persistence;

public interface ICalculationRepository
{
    public Task<ResponseDTO<int>> CountSalary(CalculationDTO model);
}
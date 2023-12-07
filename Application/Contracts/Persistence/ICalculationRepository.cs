using Application.DTO.Calculation;
using Application.DTO.Common;
using Domain.Models.CalculationModels;

namespace Application.Contracts.Persistence;

public interface ICalculationRepository
{
    public Task<ResponseDTO<int>> CountSalary(CalculationDTO model);


    public Task<ResponseDTO<AreaSalary[]>> GetSalaryArea();
}
using Application.DTO.Calculation;
using Application.DTO.Common;
using MediatR;

namespace Application.Features.CalculationCQRS.Query.CalculateSalaryAsync;

public class CalculateSalaryAsyncQuery : IRequest<ResponseDTO<int>>
{
    public CalculationDTO model { get; set; }

    public CalculateSalaryAsyncQuery(CalculationDTO model)
    {
        this.model = model;
    }
}
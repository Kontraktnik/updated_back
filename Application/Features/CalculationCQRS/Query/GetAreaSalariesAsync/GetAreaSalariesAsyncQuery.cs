using Application.DTO.Calculation;
using Application.DTO.Common;
using Domain.Models.CalculationModels;
using MediatR;

namespace Application.Features.CalculationCQRS.Query.GetAreaSalariesAsync;

public class GetAreaSalariesAsyncQuery : IRequest<ResponseDTO<AreaSalary[]>>
{

    public GetAreaSalariesAsyncQuery()
    {

    }
}
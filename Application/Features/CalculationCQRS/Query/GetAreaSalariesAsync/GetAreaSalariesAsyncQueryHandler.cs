using Application.Contracts.Persistence;
using Application.DTO.Common;
using Application.Features.CalculationCQRS.Query.CalculateSalaryAsync;
using Domain.Models.CalculationModels;
using MediatR;

namespace Application.Features.CalculationCQRS.Query.GetAreaSalariesAsync;

public class GetAreaSalariesAsyncQueryHandler : IRequestHandler<GetAreaSalariesAsyncQuery, ResponseDTO<AreaSalary[]>>
{
    private ICalculationRepository _repository { get; set; }

    public GetAreaSalariesAsyncQueryHandler(ICalculationRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<ResponseDTO<AreaSalary[]>> Handle(GetAreaSalariesAsyncQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetSalaryArea();
    }
}
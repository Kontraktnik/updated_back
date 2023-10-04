using Application.Contracts.Persistence;
using Application.DTO.Common;
using MediatR;

namespace Application.Features.CalculationCQRS.Query.CalculateSalaryAsync;

public class CalculateSalaryAsyncQueryHandler : IRequestHandler<CalculateSalaryAsyncQuery,ResponseDTO<int>>
{
    private ICalculationRepository _repository { get; set; }

    public CalculateSalaryAsyncQueryHandler(ICalculationRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<ResponseDTO<int>> Handle(CalculateSalaryAsyncQuery request, CancellationToken cancellationToken)
    {
        return await _repository.CountSalary(request.model);
    }
}
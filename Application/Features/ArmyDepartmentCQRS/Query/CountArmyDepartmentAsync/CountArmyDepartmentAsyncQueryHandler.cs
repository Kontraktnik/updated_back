using Application.Contracts.Persistence;
using Application.Resource;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Features.ArmyDepartmentCQRS.Query.CountArmyDepartmentAsync;

public class CountArmyDepartmentAsyncQueryHandler : IRequestHandler<CountArmyDepartmentAsyncQuery,int>
{
    private readonly IArmyDepartmentRepository _armyDepartmentRepository;

    public CountArmyDepartmentAsyncQueryHandler(IArmyDepartmentRepository armyDepartmentRepository)
    {
        _armyDepartmentRepository = armyDepartmentRepository;
    }
    
    public async Task<int> Handle(CountArmyDepartmentAsyncQuery request, CancellationToken cancellationToken)
    {
        return await _armyDepartmentRepository.CountAsync(request.specification);
    }
}
using Application.Contracts.Persistence;
using AutoMapper;
using MediatR;

namespace Application.Features.ArmyTypeCQRS.Query.CountArmyTypeAsync;

public class CountArmyTypeAsyncQueryHandler : IRequestHandler<CountArmyTypeAsyncQuery,int>
{
    private readonly IArmyTypeRepository _armyTypeRepository;
    private readonly IMapper _mapper;

    public CountArmyTypeAsyncQueryHandler(IArmyTypeRepository armyTypeRepository,IMapper mapper)
    {
        _armyTypeRepository = armyTypeRepository;
        _mapper = mapper;
    }

    public async Task<int> Handle(CountArmyTypeAsyncQuery request, CancellationToken cancellationToken)
    {
        return await _armyTypeRepository.CountAsync(request.specification);
    }
}
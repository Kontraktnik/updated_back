using Application.Contracts.Persistence;
using AutoMapper;
using MediatR;

namespace Application.Features.ArmyRegionCQRS.Query.CountArmyRegionAsync;

public class CountArmyRegionAsyncQueryHandler : IRequestHandler<CountArmyRegionAsyncQuery,int>
{
    private readonly IArmyRegionRepository _armyRegionRepository;
    private readonly IMapper _mapper;

    public CountArmyRegionAsyncQueryHandler(IArmyRegionRepository armyRegionRepository,IMapper mapper)
    {
        _armyRegionRepository = armyRegionRepository;
        _mapper = mapper;
    }


    public Task<int> Handle(CountArmyRegionAsyncQuery request, CancellationToken cancellationToken)
    {
        return _armyRegionRepository.CountAsync(request.specification);
    }
}
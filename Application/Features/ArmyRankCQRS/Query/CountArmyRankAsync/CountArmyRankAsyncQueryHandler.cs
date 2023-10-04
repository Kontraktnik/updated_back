using Application.Contracts.Persistence;
using AutoMapper;
using MediatR;

namespace Application.Features.ArmyRankCQRS.Query.CountArmyRankAsync;

public class CountArmyRankAsyncQueryHandler : IRequestHandler<CountArmyRankAsyncQuery,int>
{
    private readonly IArmyRankRepository _armyRankRepository;
    private readonly IMapper _mapper;

    public CountArmyRankAsyncQueryHandler(IArmyRankRepository armyRankRepository,IMapper mapper)
    {
        _armyRankRepository = armyRankRepository;
        _mapper = mapper;
    }


    public async Task<int> Handle(CountArmyRankAsyncQuery request, CancellationToken cancellationToken)
    {
        return await _armyRankRepository.CountAsync(request.specification);
    }
}
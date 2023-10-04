using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Common;
using Application.DTO.System;
using AutoMapper;
using MediatR;

namespace Application.Features.ArmyRankCQRS.Query.ListArmyRankAllAsync;

public class ListArmyRankAllAsyncQueryHandler : IRequestHandler<ListArmyRankAllAsyncQuery,ResponseDTO<ICollection<ArmyRankDTO>>>
{
    private readonly IArmyRankRepository _armyRankRepository;
    private readonly IMapper _mapper;

    public ListArmyRankAllAsyncQueryHandler(IArmyRankRepository armyRankRepository,IMapper mapper)
    {
        _armyRankRepository = armyRankRepository;
        _mapper = mapper;
    }

    public async Task<ResponseDTO<ICollection<ArmyRankDTO>>> Handle(ListArmyRankAllAsyncQuery request, CancellationToken cancellationToken)
    {
        var armyRanks = await _armyRankRepository.ListAllAsync();
        var armyRanksDTO = _mapper.Map<ICollection<ArmyRankDTO>>(armyRanks);
        return new ResponseDTO<ICollection<ArmyRankDTO>>
        {
            StatusCode = (int)HttpStatusCode.OK,
            Success = true,
            Data = armyRanksDTO,
        };
    }
}
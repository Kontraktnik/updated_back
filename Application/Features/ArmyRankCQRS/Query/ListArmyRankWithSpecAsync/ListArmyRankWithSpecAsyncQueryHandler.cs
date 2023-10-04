using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Common;
using Application.DTO.System;
using Application.Features.ArmyRankCQRS.Query.ListArmyRankAllAsync;
using AutoMapper;
using MediatR;

namespace Application.Features.ArmyRankCQRS.Query.ListArmyRankWithSpecAsync;

public class ListArmyRankWithSpecAsyncQueryHandler : IRequestHandler<ListArmyRankWithSpecAsyncQuery,ResponseDTO<ICollection<ArmyRankDTO>>>
{
    private readonly IArmyRankRepository _armyRankRepository;
    private readonly IMapper _mapper;

    public ListArmyRankWithSpecAsyncQueryHandler(IArmyRankRepository armyRankRepository,IMapper mapper)
    {
        _armyRankRepository = armyRankRepository;
        _mapper = mapper;
    }
    
    public async Task<ResponseDTO<ICollection<ArmyRankDTO>>> Handle(ListArmyRankWithSpecAsyncQuery request, CancellationToken cancellationToken)
    {
        var armyRanks = await _armyRankRepository.ListWithSpecAsync(request.specification);
        var armyRanksDTO = _mapper.Map<ICollection<ArmyRankDTO>>(armyRanks);
        return new ResponseDTO<ICollection<ArmyRankDTO>>
        {
            StatusCode = (int)HttpStatusCode.OK,
            Success = true,
            Data = armyRanksDTO,
        };
    }
}
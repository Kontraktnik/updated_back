using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Common;
using Application.DTO.System;
using Application.Resource;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Features.ArmyRankCQRS.Query.GetArmyRankByIdAsync;

public class GetArmyRankByIdAsyncQueryHandler : IRequestHandler<GetArmyRankByIdAsyncQuery,ResponseDTO<ArmyRankDTO>>
{
    private readonly IArmyRankRepository _armyRankRepository;
    private readonly IMapper _mapper;
    private IStringLocalizer<Localize> localizer;

    public GetArmyRankByIdAsyncQueryHandler(IArmyRankRepository armyRankRepository,IMapper mapper,IStringLocalizer<Localize> localizer)
    {
        _armyRankRepository = armyRankRepository;
        _mapper = mapper;
        this.localizer = localizer;
    }


    public async Task<ResponseDTO<ArmyRankDTO>> Handle(GetArmyRankByIdAsyncQuery request, CancellationToken cancellationToken)
    {
        var armyRank = await _armyRankRepository.GetByIdAsync(request.Id);
        if (armyRank != null)
        {
            return new ResponseDTO<ArmyRankDTO>
            {
                StatusCode = (int)HttpStatusCode.OK,
                Success = true,
                Data = _mapper.Map<ArmyRankDTO>(armyRank),
            };
        }
        else
        {
            return new ResponseDTO<ArmyRankDTO>
            {
                StatusCode = (int)HttpStatusCode.NotFound,
                Success = false,
                Data = null,
                Message = localizer["NotFound"]
            };
        }
    }
}
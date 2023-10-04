using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Common;
using Application.DTO.System;
using Application.Features.ArmyRegionCQRS.Query.GetArmyRegionByIdAsync;
using Application.Resource;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Features.ArmyRegionCQRS.Query.GetArmyRegionWithSpecAsync;

public class GetArmyRegionWithSpecAsyncQueryHandler : IRequestHandler<GetArmyRegionWithSpecAsyncQuery,ResponseDTO<ArmyRegionDTO>>
{
    private readonly IArmyRegionRepository _armyRegionRepository;
    private readonly IMapper _mapper;
    private IStringLocalizer<Localize> localizer;

    public GetArmyRegionWithSpecAsyncQueryHandler(IArmyRegionRepository armyRegionRepository,IMapper mapper,IStringLocalizer<Localize> localizer)
    {
        _armyRegionRepository = armyRegionRepository;
        _mapper = mapper;
        this.localizer = localizer;
    }

    public async Task<ResponseDTO<ArmyRegionDTO>> Handle(GetArmyRegionWithSpecAsyncQuery request, CancellationToken cancellationToken)
    {
        var armyRegion = await _armyRegionRepository.GetEntityWithSpecAsync(request.specification);
        if (armyRegion != null)
        {
            return new ResponseDTO<ArmyRegionDTO>
            {
                StatusCode = (int)HttpStatusCode.OK,
                Success = true,
                Data = _mapper.Map<ArmyRegionDTO>(armyRegion),
            };
        }
        else
        {
            return new ResponseDTO<ArmyRegionDTO>
            {
                StatusCode = (int)HttpStatusCode.NotFound,
                Success = false,
                Data = null,
                Message = this.localizer["NotFound"]
            };
        }
    }
}
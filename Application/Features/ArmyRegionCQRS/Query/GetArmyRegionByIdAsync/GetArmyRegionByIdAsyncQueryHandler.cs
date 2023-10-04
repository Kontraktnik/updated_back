using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Common;
using Application.DTO.System;
using Application.Resource;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Features.ArmyRegionCQRS.Query.GetArmyRegionByIdAsync;

public class GetArmyRegionByIdAsyncQueryHandler : IRequestHandler<GetArmyRegionByIdAsyncQuery,ResponseDTO<ArmyRegionDTO>>
{
    private readonly IArmyRegionRepository _armyRegionRepository;
    private readonly IMapper _mapper;
    private IStringLocalizer<Localize> localizer;

    public GetArmyRegionByIdAsyncQueryHandler(IArmyRegionRepository armyRegionRepository,IMapper mapper,IStringLocalizer<Localize> localizer)
    {
        _armyRegionRepository = armyRegionRepository;
        _mapper = mapper;
        this.localizer = localizer;
    }

    public async Task<ResponseDTO<ArmyRegionDTO>> Handle(GetArmyRegionByIdAsyncQuery request, CancellationToken cancellationToken)
    {
        var armyRegion = await _armyRegionRepository.GetByIdAsync(request.Id);
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
                Message = localizer["NotFound"]
            };
        }
    }
}
using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Common;
using Application.DTO.System;
using AutoMapper;
using MediatR;

namespace Application.Features.ArmyRegionCQRS.Query.ListArmyRegionWithSpecAsync;

public class ListArmyRegionWithSpecAsyncQueryHandler : IRequestHandler<ListArmyRegionWithSpecAsyncQuery,ResponseDTO<ICollection<ArmyRegionDTO>>>
{
    private readonly IArmyRegionRepository _armyRegionRepository;
    private readonly IMapper _mapper;

    public ListArmyRegionWithSpecAsyncQueryHandler(IArmyRegionRepository armyRegionRepository,IMapper mapper)
    {
        _armyRegionRepository = armyRegionRepository;
        _mapper = mapper;
    }
    
    public async Task<ResponseDTO<ICollection<ArmyRegionDTO>>> Handle(ListArmyRegionWithSpecAsyncQuery request, CancellationToken cancellationToken)
    {
        var armyRegions = await _armyRegionRepository.ListWithSpecAsync(request.specification);
        var armyRegionsDTO = _mapper.Map<ICollection<ArmyRegionDTO>>(armyRegions);
        return new ResponseDTO<ICollection<ArmyRegionDTO>>
        {
            StatusCode = (int)HttpStatusCode.OK,
            Success = true,
            Data = armyRegionsDTO,
        };
    }
}
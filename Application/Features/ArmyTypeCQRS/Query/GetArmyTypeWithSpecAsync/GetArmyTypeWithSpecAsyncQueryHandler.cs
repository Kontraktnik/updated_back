using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Common;
using Application.DTO.System;
using Application.Resource;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Features.ArmyTypeCQRS.Query.GetArmyTypeWithSpecAsync;

public class GetArmyTypeWithSpecAsyncQueryHandler : IRequestHandler<GetArmyTypeWithSpecAsyncQuery,ResponseDTO<ArmyTypeDTO>>
{
    private readonly IArmyTypeRepository _armyTypeRepository;
    private readonly IMapper _mapper;
    private IStringLocalizer<Localize> localizer;

    public GetArmyTypeWithSpecAsyncQueryHandler(IArmyTypeRepository armyTypeRepository,IMapper mapper,IStringLocalizer<Localize> localizer)
    {
        _armyTypeRepository = armyTypeRepository;
        _mapper = mapper;
        this.localizer = localizer;

    }

    public async Task<ResponseDTO<ArmyTypeDTO>> Handle(GetArmyTypeWithSpecAsyncQuery request, CancellationToken cancellationToken)
    {
        var armyType = await _armyTypeRepository.GetEntityWithSpecAsync(request.specification);
        if (armyType != null)
        {
            return new ResponseDTO<ArmyTypeDTO>
            {
                StatusCode = (int)HttpStatusCode.OK,
                Success = true,
                Data = _mapper.Map<ArmyTypeDTO>(armyType),
            };
        }
        else
        {
            return new ResponseDTO<ArmyTypeDTO>
            {
                StatusCode = (int)HttpStatusCode.NotFound,
                Success = false,
                Data = null,
                Message = this.localizer["NotFound"]
            };
        }
    }
}
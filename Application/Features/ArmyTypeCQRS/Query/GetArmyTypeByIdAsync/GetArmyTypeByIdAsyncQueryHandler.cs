using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Common;
using Application.DTO.System;
using Application.Resource;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Features.ArmyTypeCQRS.Query.GetArmyTypeByIdAsync;

public class GetArmyTypeByIdAsyncQueryHandler : IRequestHandler<GetArmyTypeByIdAsyncQuery,ResponseDTO<ArmyTypeDTO>>
{
    private readonly IArmyTypeRepository _armyTypeRepository;
    private readonly IMapper _mapper;
    private IStringLocalizer<Localize> localizer;

    public GetArmyTypeByIdAsyncQueryHandler(IArmyTypeRepository armyTypeRepository,IMapper mapper,IStringLocalizer<Localize> localizer)
    {
        _armyTypeRepository = armyTypeRepository;
        _mapper = mapper;
        this.localizer = localizer;

    }

    public async Task<ResponseDTO<ArmyTypeDTO>> Handle(GetArmyTypeByIdAsyncQuery request, CancellationToken cancellationToken)
    {
        var armyType = await _armyTypeRepository.GetByIdAsync(request.Id);
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
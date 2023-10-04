using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Common;
using Application.DTO.System;
using Application.Resource;
using AutoMapper;
using Domain.Models.SystemModels;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Features.ArmyRegionCQRS.Command.AddArmyRegion;

public class AddArmyRegionCommandHandler : IRequestHandler<AddArmyRegionCommand,ResponseDTO<ArmyRegionDTO>>
{
    private readonly IArmyRegionRepository _armyRegionRepository;
    private readonly IMapper _mapper;
    private IStringLocalizer<Localize> localizer;

    public AddArmyRegionCommandHandler(IArmyRegionRepository armyRegionRepository,IMapper mapper, IStringLocalizer<Localize> localizer)
    {
        _armyRegionRepository = armyRegionRepository;
        _mapper = mapper;
        this.localizer = localizer;
    }

    public async Task<ResponseDTO<ArmyRegionDTO>> Handle(AddArmyRegionCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var newArmyRegion = _mapper.Map<ArmyRegion>(request.model);
            var armyRegion = await _armyRegionRepository.AddAsync(newArmyRegion);
            return new ResponseDTO<ArmyRegionDTO>
            {
                Success = true,
                StatusCode = (int) HttpStatusCode.Created,
                Message = localizer["Created"],
                Data = _mapper.Map<ArmyRegionDTO>(armyRegion)
            };
        }
        catch (Exception ex)
        {
            return new ResponseDTO<ArmyRegionDTO>
            {
                Success = false,
                StatusCode = (int) HttpStatusCode.InternalServerError,
                Message = this.localizer["UnexpectedError"] +":" + ex.Message
            };
        }
    }
}
using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Common;
using Application.DTO.System;
using Application.Resource;
using AutoMapper;
using Domain.Models.SystemModels;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Features.ArmyRegionCQRS.Command.UpdateArmyRegion;

public class UpdateArmyRegionCommandHandler : IRequestHandler<UpdateArmyRegionCommand,ResponseDTO<ArmyRegionDTO>>
{
    private readonly IArmyRegionRepository _armyRegionRepository;
    private readonly IMapper _mapper;
    private IStringLocalizer<Localize> localizer;

    public UpdateArmyRegionCommandHandler(IArmyRegionRepository armyRegionRepository,IMapper mapper,IStringLocalizer<Localize> localizer)
    {
        _armyRegionRepository = armyRegionRepository;
        _mapper = mapper;
        this.localizer = localizer;

    }


    public async Task<ResponseDTO<ArmyRegionDTO>> Handle(UpdateArmyRegionCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if ((await _armyRegionRepository.GetByIdAsync(request.Id)) != null)
            {
                var updatedArmyRegion = _mapper.Map<ArmyRegion>(request.model);
                var armyRegion = await _armyRegionRepository.UpdateAsync(updatedArmyRegion);
                return new ResponseDTO<ArmyRegionDTO>
                {
                    Success = true,
                    StatusCode = (int) HttpStatusCode.Created,
                    Message = localizer["Updated"],
                    Data = _mapper.Map<ArmyRegionDTO>(armyRegion)
                };  
            }
            return new ResponseDTO<ArmyRegionDTO>
            {
                Success = false,
                StatusCode = (int) HttpStatusCode.NotFound,
                Message = localizer["NotFound"],
            };  
        }
        catch (Exception ex)
        {
            return new ResponseDTO<ArmyRegionDTO>
            {
                Success = false,
                StatusCode = (int) HttpStatusCode.InternalServerError,
                Message = this.localizer["UnexpectedError"] +":" + ex.Message,
            };
        }
    }
}
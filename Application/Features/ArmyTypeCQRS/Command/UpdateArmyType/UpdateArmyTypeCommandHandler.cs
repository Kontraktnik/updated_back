using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Common;
using Application.DTO.System;
using Application.Resource;
using AutoMapper;
using Domain.Models.SystemModels;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Features.ArmyTypeCQRS.Command.UpdateArmyType;

public class UpdateArmyTypeCommandHandler : IRequestHandler<UpdateArmyTypeCommand,ResponseDTO<ArmyTypeDTO>>
{
    private readonly IArmyTypeRepository _armyTypeRepository;
    private readonly IMapper _mapper;
    private IStringLocalizer<Localize> localizer;

    public UpdateArmyTypeCommandHandler(IArmyTypeRepository armyTypeRepository,IMapper mapper, IStringLocalizer<Localize> localizer)
    {
        _armyTypeRepository = armyTypeRepository;
        _mapper = mapper;
        this.localizer = localizer;
    }

    public async Task<ResponseDTO<ArmyTypeDTO>> Handle(UpdateArmyTypeCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if ((await _armyTypeRepository.GetByIdAsync(request.Id)) != null)
            {
                var updatedArmyType = _mapper.Map<ArmyType>(request.model);
                var armyType = await _armyTypeRepository.UpdateAsync(updatedArmyType);
                return new ResponseDTO<ArmyTypeDTO>
                {
                    Success = true,
                    StatusCode = (int) HttpStatusCode.Created,
                    Message = this.localizer["Updated"],
                    Data = _mapper.Map<ArmyTypeDTO>(armyType)
                };  
            }
            return new ResponseDTO<ArmyTypeDTO>
            {
                Success = false,
                StatusCode = (int) HttpStatusCode.NotFound,
                Message = this.localizer["NotFound"],
            };  
        }
        catch (Exception ex)
        {
            return new ResponseDTO<ArmyTypeDTO>
            {
                Success = false,
                StatusCode = (int) HttpStatusCode.InternalServerError,
                Message = this.localizer["UnexpectedError"] +":" + ex.Message,
            };
        }
    }
}
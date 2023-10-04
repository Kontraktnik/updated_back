using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Common;
using Application.DTO.System;
using Application.Resource;
using AutoMapper;
using Domain.Models.SystemModels;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Features.ArmyTypeCQRS.Command.AddArmyType;

public class AddArmyTypeCommandHandler : IRequestHandler<AddArmyTypeCommand,ResponseDTO<ArmyTypeDTO>>
{
    private readonly IArmyTypeRepository _armyTypeRepository;
    private readonly IMapper _mapper;
    private IStringLocalizer<Localize> localizer;

    public AddArmyTypeCommandHandler(IArmyTypeRepository armyTypeRepository,IMapper mapper,IStringLocalizer<Localize> localizer)
    {
        _armyTypeRepository = armyTypeRepository;
        _mapper = mapper;
        this.localizer = localizer;
    }

    public async Task<ResponseDTO<ArmyTypeDTO>> Handle(AddArmyTypeCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var newArmyType = _mapper.Map<ArmyType>(request.model);
            var armyType = await _armyTypeRepository.AddAsync(newArmyType);
            return new ResponseDTO<ArmyTypeDTO>
            {
                Success = true,
                StatusCode = (int) HttpStatusCode.Created,
                Message = this.localizer["Created"],
                Data = _mapper.Map<ArmyTypeDTO>(armyType)
            };
        }
        catch (Exception ex)
        {
            return new ResponseDTO<ArmyTypeDTO>
            {
                Success = false,
                StatusCode = (int) HttpStatusCode.InternalServerError,
                Message = this.localizer["UnexpectedError"] +":" + ex.Message
            };
        }
    }
}
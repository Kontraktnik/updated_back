using Application.DTO.Common;
using Application.DTO.System;
using MediatR;

namespace Application.Features.ArmyRegionCQRS.Command.AddArmyRegion;

public class AddArmyRegionCommand : IRequest<ResponseDTO<ArmyRegionDTO>>
{
    public ArmyRegionDTO model { get; set; }

    public AddArmyRegionCommand(ArmyRegionDTO model)
    {
        this.model = model;
    }
}
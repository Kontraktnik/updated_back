using Application.DTO.Common;
using Application.DTO.System;
using MediatR;

namespace Application.Features.ArmyRegionCQRS.Command.UpdateArmyRegion;

public class UpdateArmyRegionCommand : IRequest<ResponseDTO<ArmyRegionDTO>>
{
    public long Id { get; set; }
    public ArmyRegionDTO model { get; set; }

    public UpdateArmyRegionCommand(ArmyRegionDTO model,long Id)
    {
        this.model = model;
        this.model.Id = Id;
        this.Id = Id;
    }
}
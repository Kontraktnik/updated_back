using Application.DTO.Common;
using Application.DTO.System;
using MediatR;

namespace Application.Features.ArmyTypeCQRS.Command.UpdateArmyType;

public class UpdateArmyTypeCommand : IRequest<ResponseDTO<ArmyTypeDTO>>
{
    public long Id { get; set; }
    public ArmyTypeDTO model { get; set; }

    public UpdateArmyTypeCommand(ArmyTypeDTO model,long Id)
    {
        this.Id = Id;
        this.model = model;
    }
}
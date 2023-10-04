using Application.DTO.Common;
using Application.DTO.System;
using MediatR;

namespace Application.Features.ArmyTypeCQRS.Command.AddArmyType;

public class AddArmyTypeCommand : IRequest<ResponseDTO<ArmyTypeDTO>>
{
    public ArmyTypeDTO model { get; set; }

    public AddArmyTypeCommand(ArmyTypeDTO model)
    {
        this.model = model;
    }
}
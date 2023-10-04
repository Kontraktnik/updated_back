using Application.DTO.Common;
using Application.DTO.System;
using MediatR;

namespace Application.Features.ArmyDepartmentCQRS.Command.AddArmyDepartment;

public class AddArmyDepartmentCommand : IRequest<ResponseDTO<ArmyDepartmentDTO>>
{
    public ArmyDepartmentDTO model { get; set; }

    public AddArmyDepartmentCommand(ArmyDepartmentDTO model)
    {
        this.model = model;
    }
}
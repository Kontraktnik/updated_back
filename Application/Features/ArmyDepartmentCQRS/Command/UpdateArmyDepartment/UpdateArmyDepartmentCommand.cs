using Application.DTO.Common;
using Application.DTO.System;
using MediatR;

namespace Application.Features.ArmyDepartmentCQRS.Command.UpdateArmyDepartment;

public class UpdateArmyDepartmentCommand : IRequest<ResponseDTO<ArmyDepartmentDTO>>
{
    public  long Id { get; set; }
    public ArmyDepartmentDTO model { get; set; }

    public UpdateArmyDepartmentCommand(long Id,ArmyDepartmentDTO model)
    {
        this.model = model;
        this.Id = Id;
    }
}
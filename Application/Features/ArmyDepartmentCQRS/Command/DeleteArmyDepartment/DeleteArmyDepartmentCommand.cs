using Application.DTO.Common;
using MediatR;

namespace Application.Features.ArmyDepartmentCQRS.Command.DeleteArmyDepartment;

public class DeleteArmyDepartmentCommand : IRequest<ResponseDTO<bool>>
{
    public  long Id { get; set; }

    public DeleteArmyDepartmentCommand(long Id)
    {
        this.Id = Id;
    }
}
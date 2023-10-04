using Application.DTO.Common;
using MediatR;

namespace Application.Features.ArmyTypeCQRS.Command.DeleteArmyType;

public class DeleteArmyTypeCommand : IRequest<ResponseDTO<bool>>
{
    public  long Id { get; set; }

    public DeleteArmyTypeCommand(long Id)
    {
        this.Id = Id;
    }
}
using Application.DTO.Common;
using MediatR;

namespace Application.Features.PositionCQRS.Command.DeletePosition;

public class DeletePositionCommand : IRequest<ResponseDTO<bool>>
{
    public  long Id { get; set; }

    public DeletePositionCommand(long Id)
    {
        this.Id = Id;
    }
}
using Application.DTO.Common;
using MediatR;

namespace Application.Features.RelativeCQRS.Command.DeleteRelative;

public class DeleteRelativeCommand : IRequest<ResponseDTO<bool>>
{
    public  long Id { get; set; }

    public DeleteRelativeCommand(long Id)
    {
        this.Id = Id;
    }
}
using Application.DTO.Common;
using MediatR;

namespace Application.Features.VTShCQRS.Command.DeleteVTSh;

public class DeleteVTShCommand : IRequest<ResponseDTO<bool>>
{
    public  long Id { get; set; }

    public DeleteVTShCommand(long Id)
    {
        this.Id = Id;
    }
}
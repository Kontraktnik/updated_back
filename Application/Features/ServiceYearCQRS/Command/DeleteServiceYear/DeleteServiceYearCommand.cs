using Application.DTO.Common;
using MediatR;

namespace Application.Features.ServiceYearCQRS.Command.DeleteServiceYear;

public class DeleteServiceYearCommand : IRequest<ResponseDTO<bool>>
{
    public  long Id { get; set; }

    public DeleteServiceYearCommand(long Id)
    {
        this.Id = Id;
    }
}
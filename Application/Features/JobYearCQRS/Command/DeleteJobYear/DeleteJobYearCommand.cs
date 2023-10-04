using Application.DTO.Common;
using MediatR;

namespace Application.Features.JobYearCQRS.Command.DeleteJobYear;

public class DeleteJobYearCommand : IRequest<ResponseDTO<bool>>
{
    public  long Id { get; set; }

    public DeleteJobYearCommand(long Id)
    {
        this.Id = Id;
    }
}
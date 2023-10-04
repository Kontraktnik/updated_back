using Application.DTO.Common;
using MediatR;

namespace Application.Features.RankSalaryCQRS.Command.DeleteRankSalary;

public class DeleteRankSalaryCommand : IRequest<ResponseDTO<bool>>
{
    public  long Id { get; set; }

    public DeleteRankSalaryCommand(long Id)
    {
        this.Id = Id;
    }
}
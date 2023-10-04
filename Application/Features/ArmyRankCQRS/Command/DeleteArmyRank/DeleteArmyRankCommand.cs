using Application.DTO.Common;
using MediatR;

namespace Application.Features.ArmyRankCQRS.Command.DeleteArmyRank;

public class DeleteArmyRankCommand : IRequest<ResponseDTO<bool>>
{
    public  long Id { get; set; }

    public DeleteArmyRankCommand(long Id)
    {
        this.Id = Id;
    }
}
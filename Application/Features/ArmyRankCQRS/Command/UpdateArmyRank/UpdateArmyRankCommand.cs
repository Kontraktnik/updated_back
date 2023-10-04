using Application.DTO.Common;
using Application.DTO.System;
using Domain.Models.SystemModels;
using MediatR;

namespace Application.Features.ArmyRankCQRS.Command.UpdateArmyRank;

public class UpdateArmyRankCommand : IRequest<ResponseDTO<ArmyRankDTO>>
{
    public  long Id { get; set; }
    public ArmyRankDTO model { get; set; }

    public UpdateArmyRankCommand(long Id,ArmyRankDTO model)
    {
        this.model = model;
        this.Id = Id;
    }
}
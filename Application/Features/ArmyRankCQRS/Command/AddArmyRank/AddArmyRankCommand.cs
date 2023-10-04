using Application.DTO.Common;
using Application.DTO.System;
using Domain.Models.SystemModels;
using MediatR;

namespace Application.Features.ArmyRankCQRS.Command.AddArmyRank;

public class AddArmyRankCommand : IRequest<ResponseDTO<ArmyRankDTO>>
{
    public ArmyRankDTO model { get; set; }

    public AddArmyRankCommand(ArmyRankDTO model)
    {
        this.model = model;
    }
}
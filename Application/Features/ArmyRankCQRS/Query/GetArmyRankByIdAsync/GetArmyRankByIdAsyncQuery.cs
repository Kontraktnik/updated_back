using Application.DTO.Common;
using Application.DTO.System;
using Domain.Models.SystemModels;
using MediatR;

namespace Application.Features.ArmyRankCQRS.Query.GetArmyRankByIdAsync;

public class GetArmyRankByIdAsyncQuery : IRequest<ResponseDTO<ArmyRankDTO>>
{
    public long Id { get; set; }

    public GetArmyRankByIdAsyncQuery(long Id)
    {
        this.Id = Id;
    }
}
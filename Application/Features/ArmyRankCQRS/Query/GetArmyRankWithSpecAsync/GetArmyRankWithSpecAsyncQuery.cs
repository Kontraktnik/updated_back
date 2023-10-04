using Application.Contracts.Specification;
using Application.DTO.Common;
using Application.DTO.System;
using Domain.Models.SystemModels;
using MediatR;

namespace Application.Features.ArmyRankCQRS.Query.GetArmyRankWithSpecAsync;

public class GetArmyRankWithSpecAsyncQuery : IRequest<ResponseDTO<ArmyRankDTO>>
{
    public ISpecification<ArmyRank> specification { get; set; }

    public GetArmyRankWithSpecAsyncQuery(ISpecification<ArmyRank> specification)
    {
        this.specification = specification;
    }
}
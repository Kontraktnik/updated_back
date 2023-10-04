using Application.Contracts.Specification;
using Application.DTO.Common;
using Application.DTO.System;
using Domain.Models.SystemModels;
using MediatR;

namespace Application.Features.ArmyRankCQRS.Query.ListArmyRankWithSpecAsync;

public class ListArmyRankWithSpecAsyncQuery : IRequest<ResponseDTO<ICollection<ArmyRankDTO>>>
{
    public ISpecification<ArmyRank> specification { get; set; }

    public ListArmyRankWithSpecAsyncQuery(ISpecification<ArmyRank> specification)
    {
        this.specification = specification;
    }
}
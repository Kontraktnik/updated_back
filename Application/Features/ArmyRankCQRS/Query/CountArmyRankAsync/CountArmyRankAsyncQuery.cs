using Application.Contracts.Specification;
using Domain.Models.SystemModels;
using MediatR;

namespace Application.Features.ArmyRankCQRS.Query.CountArmyRankAsync;

public class CountArmyRankAsyncQuery : IRequest<int>
{
    public ISpecification<ArmyRank> specification { get; set; }

    public CountArmyRankAsyncQuery(ISpecification<ArmyRank> specification)
    {
        this.specification = specification;
    }
}
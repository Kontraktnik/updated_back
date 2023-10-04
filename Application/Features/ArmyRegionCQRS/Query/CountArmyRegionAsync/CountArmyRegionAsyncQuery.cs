using Application.Contracts.Specification;
using Domain.Models.SystemModels;
using MediatR;

namespace Application.Features.ArmyRegionCQRS.Query.CountArmyRegionAsync;

public class CountArmyRegionAsyncQuery : IRequest<int>
{
    public ISpecification<ArmyRegion> specification { get; set; }

    public CountArmyRegionAsyncQuery(ISpecification<ArmyRegion> specification)
    {
        this.specification = specification;
    }
}
using Application.Contracts.Specification;
using Application.DTO.Common;
using Application.DTO.System;
using Domain.Models.SystemModels;
using MediatR;

namespace Application.Features.ArmyRegionCQRS.Query.ListArmyRegionWithSpecAsync;

public class ListArmyRegionWithSpecAsyncQuery : IRequest<ResponseDTO<ICollection<ArmyRegionDTO>>>
{
    public ISpecification<ArmyRegion> specification { get; set; }

    public ListArmyRegionWithSpecAsyncQuery(ISpecification<ArmyRegion> specification)
    {
        this.specification = specification;
    }
}
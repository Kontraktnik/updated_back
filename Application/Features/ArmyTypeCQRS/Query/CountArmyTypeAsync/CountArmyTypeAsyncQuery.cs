using Application.Contracts.Specification;
using Application.DTO.Common;
using Application.DTO.System;
using Domain.Models.SystemModels;
using MediatR;

namespace Application.Features.ArmyTypeCQRS.Query.CountArmyTypeAsync;

public class CountArmyTypeAsyncQuery : IRequest<int>
{
    public ISpecification<ArmyType> specification { get; set; }

    public CountArmyTypeAsyncQuery(ISpecification<ArmyType> specification)
    {
        this.specification = specification;
    }
}
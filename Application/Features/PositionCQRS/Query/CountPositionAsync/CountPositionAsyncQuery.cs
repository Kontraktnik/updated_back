using Application.Contracts.Specification;
using Application.DTO.Common;
using Application.DTO.System;
using Domain.Models.CalculationModels;
using Domain.Models.SystemModels;
using MediatR;

namespace Application.Features.PositionCQRS.Query.CountPositionAsync;

public class CountPositionAsyncQuery : IRequest<int>
{
    public ISpecification<Position> specification { get; set; }

    public CountPositionAsyncQuery(ISpecification<Position> specification)
    {
        this.specification = specification;
    }
}
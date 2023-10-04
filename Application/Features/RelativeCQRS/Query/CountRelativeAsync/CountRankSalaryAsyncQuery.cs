using Application.Contracts.Specification;
using Application.DTO.Common;
using Application.DTO.System;
using Domain.Models.CalculationModels;
using Domain.Models.SystemModels;
using MediatR;

namespace Application.Features.RelativeCQRS.Query.CountRelativeAsync;

public class CountRelativeAsyncQuery : IRequest<int>
{
    public ISpecification<Relative> specification { get; set; }

    public CountRelativeAsyncQuery(ISpecification<Relative> specification)
    {
        this.specification = specification;
    }
}
using Application.Contracts.Specification;
using Application.DTO.Common;
using Application.DTO.System;
using Domain.Models.CalculationModels;
using Domain.Models.SystemModels;
using MediatR;

namespace Application.Features.QualificationCQRS.Query.CountQualificationAsync;

public class CountQualificationAsyncQuery : IRequest<int>
{
    public ISpecification<Qualification> specification { get; set; }

    public CountQualificationAsyncQuery(ISpecification<Qualification> specification)
    {
        this.specification = specification;
    }
}
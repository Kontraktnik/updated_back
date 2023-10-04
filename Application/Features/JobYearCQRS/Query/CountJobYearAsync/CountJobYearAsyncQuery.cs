using Application.Contracts.Specification;
using Application.DTO.Common;
using Application.DTO.System;
using Domain.Models.CalculationModels;
using Domain.Models.SystemModels;
using MediatR;

namespace Application.Features.JobYearCQRS.Query.CountJobYearAsync;

public class CountJobYearAsyncQuery : IRequest<int>
{
    public ISpecification<JobYear> specification { get; set; }

    public CountJobYearAsyncQuery(ISpecification<JobYear> specification)
    {
        this.specification = specification;
    }
}
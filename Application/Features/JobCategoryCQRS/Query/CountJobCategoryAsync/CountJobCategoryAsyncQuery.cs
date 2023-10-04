using Application.Contracts.Specification;
using Application.DTO.Common;
using Application.DTO.System;
using Domain.Models.CalculationModels;
using Domain.Models.SystemModels;
using MediatR;

namespace Application.Features.JobCategoryCQRS.Query.CountJobCategoryAsync;

public class CountJobCategoryAsyncQuery : IRequest<int>
{
    public ISpecification<JobCategory> specification { get; set; }

    public CountJobCategoryAsyncQuery(ISpecification<JobCategory> specification)
    {
        this.specification = specification;
    }
}
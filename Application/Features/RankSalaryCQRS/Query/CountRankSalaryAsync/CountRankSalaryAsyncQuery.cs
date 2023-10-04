using Application.Contracts.Specification;
using Application.DTO.Common;
using Application.DTO.System;
using Domain.Models.CalculationModels;
using Domain.Models.SystemModels;
using MediatR;

namespace Application.Features.RankSalaryCQRS.Query.CountRankSalaryAsync;

public class CountRankSalaryAsyncQuery : IRequest<int>
{
    public ISpecification<RankSalary> specification { get; set; }

    public CountRankSalaryAsyncQuery(ISpecification<RankSalary> specification)
    {
        this.specification = specification;
    }
}
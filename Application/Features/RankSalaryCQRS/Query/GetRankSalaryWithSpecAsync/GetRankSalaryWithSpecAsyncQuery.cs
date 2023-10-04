using Application.Contracts.Specification;
using Application.DTO.Calculation;
using Application.DTO.Calculation.RankSalaryDTO;
using Application.DTO.Common;
using Application.DTO.System;
using Domain.Models.CalculationModels;
using Domain.Models.SystemModels;
using MediatR;

namespace Application.Features.RankSalaryCQRS.Query.GetRankSalaryWithSpecAsync;

public class GetRankSalaryWithSpecAsyncQuery : IRequest<ResponseDTO<RankSalaryRDTO>>
{
    public ISpecification<RankSalary> specification { get; set; }

    public GetRankSalaryWithSpecAsyncQuery(ISpecification<RankSalary> specification)
    {
        this.specification = specification;
    }
}
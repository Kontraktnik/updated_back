using Application.Contracts.Specification;
using Application.DTO.Calculation;
using Application.DTO.Calculation.RankSalaryDTO;
using Application.DTO.Common;
using Application.DTO.System;
using Domain.Models.CalculationModels;
using MediatR;

namespace Application.Features.RankSalaryCQRS.Query.GetRankSalaryByIdAsync;

public class GetRankSalaryByIdAsyncQuery : IRequest<ResponseDTO<RankSalaryRDTO>>
{
    public  ISpecification<RankSalary> specification { get; set; }

    public GetRankSalaryByIdAsyncQuery(ISpecification<RankSalary> specification)
    {
        this.specification = specification;
    }
}
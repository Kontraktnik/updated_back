using Application.Contracts.Specification;
using Application.DTO.Calculation;
using Application.DTO.Calculation.JobYearDTO;
using Application.DTO.Calculation.PositionDTO;
using Application.DTO.Calculation.RankSalaryDTO;
using Application.DTO.Common;
using Application.DTO.System;
using Domain.Models.CalculationModels;
using Domain.Models.SystemModels;
using MediatR;

namespace Application.Features.JobYearCQRS.Query.GetJobYearWithSpecAsync;

public class GetJobYearWithSpecAsyncQuery : IRequest<ResponseDTO<JobYearRDTO>>
{
    public ISpecification<JobYear> specification { get; set; }

    public GetJobYearWithSpecAsyncQuery(ISpecification<JobYear> specification)
    {
        this.specification = specification;
    }
}
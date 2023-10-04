using Application.Contracts.Specification;
using Application.DTO.Calculation;
using Application.DTO.Common;
using Application.DTO.System;
using Domain.Models.CalculationModels;
using Domain.Models.SystemModels;
using MediatR;

namespace Application.Features.JobCategoryCQRS.Query.GetJobCategoryWithSpecAsync;

public class GetJobCategoryWithSpecAsyncQuery : IRequest<ResponseDTO<JobCategoryDTO>>
{
    public ISpecification<JobCategory> specification { get; set; }

    public GetJobCategoryWithSpecAsyncQuery(ISpecification<JobCategory> specification)
    {
        this.specification = specification;
    }
}
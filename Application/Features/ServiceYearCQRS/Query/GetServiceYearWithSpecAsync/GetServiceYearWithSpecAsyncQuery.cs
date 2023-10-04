using Application.Contracts.Specification;
using Application.DTO.Calculation;
using Application.DTO.Common;
using Application.DTO.System;
using Domain.Models.CalculationModels;
using Domain.Models.SystemModels;
using MediatR;

namespace Application.Features.ServiceYearCQRS.Query.GetServiceYearWithSpecAsync;

public class GetServiceYearWithSpecAsyncQuery : IRequest<ResponseDTO<ServiceYearDTO>>
{
    public ISpecification<ServiceYear> specification { get; set; }

    public GetServiceYearWithSpecAsyncQuery(ISpecification<ServiceYear> specification)
    {
        this.specification = specification;
    }
}
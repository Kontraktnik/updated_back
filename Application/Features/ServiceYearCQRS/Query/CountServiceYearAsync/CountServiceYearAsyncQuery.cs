using Application.Contracts.Specification;
using Application.DTO.Common;
using Application.DTO.System;
using Domain.Models.CalculationModels;
using Domain.Models.SystemModels;
using MediatR;

namespace Application.Features.ServiceYearCQRS.Query.CountServiceYearAsync;

public class CountServiceYearAsyncQuery : IRequest<int>
{
    public ISpecification<ServiceYear> specification { get; set; }

    public CountServiceYearAsyncQuery(ISpecification<ServiceYear> specification)
    {
        this.specification = specification;
    }
}
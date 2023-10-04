using Application.Contracts.Specification;
using Application.DTO.Calculation;
using Application.DTO.Common;
using Application.DTO.System;
using Domain.Models.CalculationModels;
using Domain.Models.SystemModels;
using MediatR;

namespace Application.Features.ServiceYearCQRS.Query.ListServiceYearWithSpecAsync;

public class ListServiceYearWithSpecAsyncQuery : IRequest<ResponseDTO<ICollection<ServiceYearDTO>>>
{
    public ISpecification<ServiceYear> specification { get; set; }

    public ListServiceYearWithSpecAsyncQuery(ISpecification<ServiceYear> specification)
    {
        this.specification = specification;
    }
}
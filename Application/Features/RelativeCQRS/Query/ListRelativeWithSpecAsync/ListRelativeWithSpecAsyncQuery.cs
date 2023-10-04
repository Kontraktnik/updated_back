using Application.Contracts.Specification;
using Application.DTO.Calculation;
using Application.DTO.Calculation.RankSalaryDTO;
using Application.DTO.Common;
using Application.DTO.System;
using Domain.Models.CalculationModels;
using Domain.Models.SystemModels;
using MediatR;

namespace Application.Features.RelativeCQRS.Query.ListRelativeWithSpecAsync;

public class ListRelativeWithSpecAsyncQuery : IRequest<ResponseDTO<ICollection<RelativeDTO>>>
{
    public ISpecification<Relative> specification { get; set; }

    public ListRelativeWithSpecAsyncQuery(ISpecification<Relative> specification)
    {
        this.specification = specification;
    }
}
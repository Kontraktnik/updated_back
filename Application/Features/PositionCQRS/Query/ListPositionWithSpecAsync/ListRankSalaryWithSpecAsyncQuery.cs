using Application.Contracts.Specification;
using Application.DTO.Calculation;
using Application.DTO.Calculation.PositionDTO;
using Application.DTO.Calculation.RankSalaryDTO;
using Application.DTO.Common;
using Application.DTO.System;
using Domain.Models.CalculationModels;
using Domain.Models.SystemModels;
using MediatR;

namespace Application.Features.PositionCQRS.Query.ListPositionWithSpecAsync;

public class ListPositionWithSpecAsyncQuery : IRequest<ResponseDTO<ICollection<PositionRDTO>>>
{
    public ISpecification<Position> specification { get; set; }

    public ListPositionWithSpecAsyncQuery(ISpecification<Position> specification)
    {
        this.specification = specification;
    }
}
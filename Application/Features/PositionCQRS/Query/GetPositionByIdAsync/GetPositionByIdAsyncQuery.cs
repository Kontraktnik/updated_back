using Application.Contracts.Specification;
using Application.DTO.Calculation;
using Application.DTO.Calculation.PositionDTO;
using Application.DTO.Calculation.RankSalaryDTO;
using Application.DTO.Common;
using Application.DTO.System;
using Domain.Models.CalculationModels;
using MediatR;

namespace Application.Features.PositionCQRS.Query.GetPositionByIdAsync;

public class GetPositionByIdAsyncQuery : IRequest<ResponseDTO<PositionRDTO>>
{
    public  ISpecification<Position> specification { get; set; }

    public GetPositionByIdAsyncQuery(ISpecification<Position> specification)
    {
        this.specification = specification;
    }
}
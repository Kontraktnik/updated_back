using Application.Contracts.Specification;
using Application.DTO.Calculation;
using Application.DTO.Common;
using Application.DTO.System;
using Domain.Models.CalculationModels;
using Domain.Models.SystemModels;
using MediatR;

namespace Application.Features.CategoryPositionCQRS.Query.GetCategoryPositionWithSpecAsync;

public class GetCategoryPositionWithSpecAsyncQuery : IRequest<ResponseDTO<CategoryPositionDTO>>
{
    public ISpecification<CategoryPosition> specification { get; set; }

    public GetCategoryPositionWithSpecAsyncQuery(ISpecification<CategoryPosition> specification)
    {
        this.specification = specification;
    }
}
using Application.Contracts.Specification;
using Application.DTO.Common;
using Application.DTO.System;
using Domain.Models.CalculationModels;
using Domain.Models.SystemModels;
using MediatR;

namespace Application.Features.CategoryPositionCQRS.Query.CountCategoryPositionAsync;

public class CountCategoryPositionAsyncQuery : IRequest<int>
{
    public ISpecification<CategoryPosition> specification { get; set; }

    public CountCategoryPositionAsyncQuery(ISpecification<CategoryPosition> specification)
    {
        this.specification = specification;
    }
}
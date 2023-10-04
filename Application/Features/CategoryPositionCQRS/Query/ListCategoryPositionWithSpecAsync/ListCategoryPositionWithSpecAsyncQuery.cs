using Application.Contracts.Specification;
using Application.DTO.Calculation;
using Application.DTO.Common;
using Application.DTO.System;
using Domain.Models.CalculationModels;
using Domain.Models.SystemModels;
using MediatR;

namespace Application.Features.CategoryPositionCQRS.Query.ListCategoryPositionWithSpecAsync;

public class ListCategoryPositionWithSpecAsyncQuery : IRequest<ResponseDTO<ICollection<CategoryPositionDTO>>>
{
    public ISpecification<CategoryPosition> specification { get; set; }

    public ListCategoryPositionWithSpecAsyncQuery(ISpecification<CategoryPosition> specification)
    {
        this.specification = specification;
    }
}
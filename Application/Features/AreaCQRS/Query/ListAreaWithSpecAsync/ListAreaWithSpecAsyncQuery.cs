using Application.Contracts.Specification;
using Application.DTO.Common;
using Application.DTO.System;
using Domain.Models.SystemModels;
using MediatR;

namespace Application.Features.AreaCQRS.Query.ListAreaWithSpecAsync;

public class ListAreaWithSpecAsyncQuery : IRequest<ResponseDTO<ICollection<AreaDTO>>>
{
    public ISpecification<Area> specification { get; set; }

    public ListAreaWithSpecAsyncQuery(ISpecification<Area> specification)
    {
        this.specification = specification;
    }
}
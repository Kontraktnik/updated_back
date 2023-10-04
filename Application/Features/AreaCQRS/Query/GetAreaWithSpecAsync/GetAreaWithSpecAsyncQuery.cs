using Application.Contracts.Specification;
using Application.DTO.Common;
using Application.DTO.System;
using Domain.Models.SystemModels;
using MediatR;

namespace Application.Features.AreaCQRS.Query.GetAreaWithSpecAsync;

public class GetAreaWithSpecAsyncQuery : IRequest<ResponseDTO<AreaDTO>>
{
    public ISpecification<Area> specification { get; set; }

    public GetAreaWithSpecAsyncQuery(ISpecification<Area> specification)
    {
        this.specification = specification;
    }
}
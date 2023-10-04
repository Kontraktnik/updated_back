using Application.Contracts.Specification;
using Application.DTO.Common;
using Application.DTO.System;
using Domain.Models.SystemModels;
using MediatR;

namespace Application.Features.VTShCQRS.Query.GetVTShWithSpecAsync;

public class GetVTShWithSpecAsyncQuery : IRequest<ResponseDTO<VTShDTO>>
{
    public ISpecification<VTSh> specification { get; set; }

    public GetVTShWithSpecAsyncQuery(ISpecification<VTSh> specification)
    {
        this.specification = specification;
    }
}
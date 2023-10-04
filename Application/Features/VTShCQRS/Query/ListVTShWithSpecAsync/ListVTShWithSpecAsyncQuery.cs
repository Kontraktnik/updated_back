using Application.Contracts.Specification;
using Application.DTO.Common;
using Application.DTO.System;
using Domain.Models.SystemModels;
using MediatR;

namespace Application.Features.VTShCQRS.Query.ListVTShWithSpecAsync;

public class ListVTShWithSpecAsyncQuery : IRequest<ResponseDTO<ICollection<VTShDTO>>>
{
    public ISpecification<VTSh> specification { get; set; }

    public ListVTShWithSpecAsyncQuery(ISpecification<VTSh> specification)
    {
        this.specification = specification;
    }
}
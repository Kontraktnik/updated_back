using Application.Contracts.Specification;
using Domain.Models.SystemModels;
using MediatR;

namespace Application.Features.VTShCQRS.Query.CountVTShAsync;

public class CountVTShAsyncQuery : IRequest<int>
{
    public ISpecification<VTSh> specification { get; set; }

    public CountVTShAsyncQuery(ISpecification<VTSh> specification)
    {
        this.specification = specification;
    }
}
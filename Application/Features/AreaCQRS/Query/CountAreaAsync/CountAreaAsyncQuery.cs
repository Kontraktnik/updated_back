using Application.Contracts.Specification;
using Application.DTO;
using Domain.Models.SystemModels;
using MediatR;

namespace Application.Features.AreaCQRS.Query.CountAreaAsync;

public class CountAreaAsyncQuery : IRequest<int>
{
    public ISpecification<Area> specification { get; set; }

    public CountAreaAsyncQuery(ISpecification<Area> specification)
    {
        this.specification = specification;
    }
    
    
}
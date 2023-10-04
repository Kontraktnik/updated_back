using Application.Contracts.Specification;
using Domain.Models.SystemModels;
using MediatR;

namespace Application.Features.EducationCQRS.Query.CountEducationAsync;

public class CountEducationAsyncQuery : IRequest<int>
{
    public ISpecification<Education> specification { get; set; }

    public CountEducationAsyncQuery(ISpecification<Education> specification)
    {
        this.specification = specification;
    }
}
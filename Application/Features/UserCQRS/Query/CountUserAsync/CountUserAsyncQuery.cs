using Application.Contracts.Specification;
using Application.DTO.Common;
using Domain.Models.UserModels;
using MediatR;

namespace Application.Features.UserCQRS.Query.CountUserAsync;

public class CountUserAsyncQuery : IRequest<int>
{
    public ISpecification<User> specification;

    public CountUserAsyncQuery(ISpecification<User> _specification)
    {
        specification = _specification;
    }
}
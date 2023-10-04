using Application.Contracts.Specification;
using Application.DTO.Common;
using Application.DTO.User;
using Domain.Models.UserModels;
using MediatR;

namespace Application.Features.UserCQRS.Query.ListUserWithSpecAsync;

public class ListUserWithSpecAsyncQuery : IRequest<ResponseDTO<ICollection<UserDTO>>>
{
    public ISpecification<User> specification { get; set; }

    public ListUserWithSpecAsyncQuery(ISpecification<User> _specification)
    {
        this.specification = _specification;
    }
}
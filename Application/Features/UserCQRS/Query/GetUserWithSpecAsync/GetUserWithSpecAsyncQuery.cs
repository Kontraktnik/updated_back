using Application.Contracts.Specification;
using Application.DTO.Common;
using Application.DTO.User;
using Domain.Models.UserModels;
using MediatR;

namespace Application.Features.UserCQRS.Query.GetUserWithSpecAsync;

public class GetUserWithSpecAsyncQuery : IRequest<ResponseDTO<UserDTO>>
{
    public ISpecification<User> specification { get; set; }

    public GetUserWithSpecAsyncQuery(ISpecification<User> _specification)
    {
        this.specification = _specification;
    }
}
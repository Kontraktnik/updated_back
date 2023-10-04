using Application.Contracts.Specification;
using Application.DTO.Common;
using Application.DTO.User;
using Domain.Models.UserModels;
using MediatR;

namespace Application.Features.UserCQRS.Query.GetUserByIdAsync;

public class GetUserByIdAsync : IRequest<ResponseDTO<UserDTO>>
{
    public ISpecification<User> specification { get; set; }

    public GetUserByIdAsync(ISpecification<User> _specification)
    {
        this.specification = _specification;
    }
}
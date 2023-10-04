using Application.Contracts.Specification;
using Application.DTO.Common;
using Application.DTO.Profile;
using Domain.Models.ProfileModels;
using MediatR;

namespace Application.Features.ProfileCQRS.Query.ListProfileWithSpecAsync;

public class ListProfileWithSpecAsyncQuery : IRequest<ResponseDTO<ICollection<ProfileDTO>>>
{
    public ISpecification<Profile> specification { get; set; }

    public ListProfileWithSpecAsyncQuery(ISpecification<Profile> specification)
    {
        this.specification = specification;
    }
}
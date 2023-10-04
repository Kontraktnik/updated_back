using Application.Contracts.Specification;
using Application.DTO.Common;
using Application.DTO.Profile;
using Domain.Models.ProfileModels;
using MediatR;

namespace Application.Features.ProfileFileCQRS.Query.ListProfileFileWithSpecAsync;

public class ListProfileFileWithSpecAsyncQuery : IRequest<ResponseDTO<ICollection<ProfileFileDTO>>>
{
    public ISpecification<ProfileFile> _specification;

    public ListProfileFileWithSpecAsyncQuery(ISpecification<ProfileFile> specification)
    {
        _specification = specification;
    }
}
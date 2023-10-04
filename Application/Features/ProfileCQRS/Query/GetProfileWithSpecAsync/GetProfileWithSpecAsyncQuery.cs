using Application.Contracts.Specification;
using Application.DTO.Calculation;
using Application.DTO.Common;
using Application.DTO.Profile;
using Application.DTO.System;
using Domain.Models.CalculationModels;
using Domain.Models.ProfileModels;
using Domain.Models.SystemModels;
using MediatR;

namespace Application.Features.ProfileCQRS.Query.GetProfileWithSpecAsync;

public class GetProfileWithSpecAsyncQuery : IRequest<ResponseDTO<ProfileDTO>>
{
    public ISpecification<Profile> specification { get; set; }

    public GetProfileWithSpecAsyncQuery(ISpecification<Profile> specification)
    {
        this.specification = specification;
    }
}
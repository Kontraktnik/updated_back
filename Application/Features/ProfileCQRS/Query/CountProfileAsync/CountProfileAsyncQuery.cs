using Application.Contracts.Specification;
using Application.DTO.Common;
using Application.DTO.System;
using Domain.Models.CalculationModels;
using Domain.Models.ProfileModels;
using Domain.Models.SystemModels;
using MediatR;

namespace Application.Features.ProfileCQRS.Query.CountProfileAsync;

public class CountProfileAsyncQuery : IRequest<int>
{
    public ISpecification<Profile> specification { get; set; }

    public CountProfileAsyncQuery(ISpecification<Profile> specification)
    {
        this.specification = specification;
    }
}
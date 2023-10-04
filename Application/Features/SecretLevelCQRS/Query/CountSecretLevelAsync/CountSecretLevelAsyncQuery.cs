using Application.Contracts.Specification;
using Application.DTO.Common;
using Application.DTO.System;
using Domain.Models.CalculationModels;
using Domain.Models.SystemModels;
using MediatR;

namespace Application.Features.SecretLevelCQRS.Query.CountSecretLevelAsync;

public class CountSecretLevelAsyncQuery : IRequest<int>
{
    public ISpecification<SecretLevel> specification { get; set; }

    public CountSecretLevelAsyncQuery(ISpecification<SecretLevel> specification)
    {
        this.specification = specification;
    }
}
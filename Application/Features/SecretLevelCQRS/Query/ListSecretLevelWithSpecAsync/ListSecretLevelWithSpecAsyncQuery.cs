using Application.Contracts.Specification;
using Application.DTO.Calculation;
using Application.DTO.Common;
using Application.DTO.System;
using Domain.Models.CalculationModels;
using Domain.Models.SystemModels;
using MediatR;

namespace Application.Features.SecretLevelCQRS.Query.ListSecretLevelWithSpecAsync;

public class ListSecretLevelWithSpecAsyncQuery : IRequest<ResponseDTO<ICollection<SecretLevelDTO>>>
{
    public ISpecification<SecretLevel> specification { get; set; }

    public ListSecretLevelWithSpecAsyncQuery(ISpecification<SecretLevel> specification)
    {
        this.specification = specification;
    }
}
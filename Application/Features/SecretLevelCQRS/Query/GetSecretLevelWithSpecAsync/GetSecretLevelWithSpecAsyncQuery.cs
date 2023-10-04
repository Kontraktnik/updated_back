using Application.Contracts.Specification;
using Application.DTO.Calculation;
using Application.DTO.Common;
using Application.DTO.System;
using Domain.Models.CalculationModels;
using Domain.Models.SystemModels;
using MediatR;

namespace Application.Features.SecretLevelCQRS.Query.GetSecretLevelWithSpecAsync;

public class GetSecretLevelWithSpecAsyncQuery : IRequest<ResponseDTO<SecretLevelDTO>>
{
    public ISpecification<SecretLevel> specification { get; set; }

    public GetSecretLevelWithSpecAsyncQuery(ISpecification<SecretLevel> specification)
    {
        this.specification = specification;
    }
}
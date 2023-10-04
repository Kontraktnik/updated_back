using Application.Contracts.Specification;
using Application.DTO.Calculation;
using Application.DTO.Common;
using Application.DTO.System;
using Domain.Models.CalculationModels;
using Domain.Models.SystemModels;
using MediatR;

namespace Application.Features.QualificationCQRS.Query.GetQualificationWithSpecAsync;

public class GetQualificationWithSpecAsyncQuery : IRequest<ResponseDTO<QualificationDTO>>
{
    public ISpecification<Qualification> specification { get; set; }

    public GetQualificationWithSpecAsyncQuery(ISpecification<Qualification> specification)
    {
        this.specification = specification;
    }
}
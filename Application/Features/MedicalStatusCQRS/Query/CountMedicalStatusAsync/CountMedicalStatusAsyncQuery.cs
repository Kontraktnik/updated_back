using Application.Contracts.Specification;
using Application.DTO.Common;
using Application.DTO.System;
using Domain.Models.CalculationModels;
using Domain.Models.SystemModels;
using MediatR;

namespace Application.Features.MedicalStatusCQRS.Query.CountMedicalStatusAsync;

public class CountMedicalStatusAsyncQuery : IRequest<int>
{
    public ISpecification<MedicalStatus> specification { get; set; }

    public CountMedicalStatusAsyncQuery(ISpecification<MedicalStatus> specification)
    {
        this.specification = specification;
    }
}
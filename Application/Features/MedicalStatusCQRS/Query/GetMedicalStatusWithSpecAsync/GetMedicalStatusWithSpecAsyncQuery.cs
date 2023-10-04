using Application.Contracts.Specification;
using Application.DTO.Calculation;
using Application.DTO.Common;
using Application.DTO.System;
using Domain.Models.CalculationModels;
using Domain.Models.SystemModels;
using MediatR;

namespace Application.Features.MedicalStatusCQRS.Query.GetMedicalStatusWithSpecAsync;

public class GetMedicalStatusWithSpecAsyncQuery : IRequest<ResponseDTO<MedicalStatusDTO>>
{
    public ISpecification<MedicalStatus> specification { get; set; }

    public GetMedicalStatusWithSpecAsyncQuery(ISpecification<MedicalStatus> specification)
    {
        this.specification = specification;
    }
}
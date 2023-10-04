using Application.Contracts.Specification;
using Application.DTO.Calculation;
using Application.DTO.Common;
using Application.DTO.System;
using Domain.Models.CalculationModels;
using Domain.Models.SystemModels;
using MediatR;

namespace Application.Features.QualificationCQRS.Query.ListQualificationWithSpecAsync;

public class ListQualificationWithSpecAsyncQuery : IRequest<ResponseDTO<ICollection<QualificationDTO>>>
{
    public ISpecification<Qualification> specification { get; set; }

    public ListQualificationWithSpecAsyncQuery(ISpecification<Qualification> specification)
    {
        this.specification = specification;
    }
}
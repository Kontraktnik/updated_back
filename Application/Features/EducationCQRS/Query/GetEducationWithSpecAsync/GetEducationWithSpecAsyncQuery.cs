using Application.Contracts.Specification;
using Application.DTO.Common;
using Application.DTO.System;
using Domain.Models.SystemModels;
using MediatR;

namespace Application.Features.EducationCQRS.Query.GetEducationWithSpecAsync;

public class GetEducationWithSpecAsyncQuery : IRequest<ResponseDTO<EducationDTO>>
{
    public ISpecification<Education> specification { get; set; }

    public GetEducationWithSpecAsyncQuery(ISpecification<Education> specification)
    {
        this.specification = specification;
    }
}
using Application.Contracts.Specification;
using Application.DTO.Common;
using Application.DTO.System;
using Domain.Models.SystemModels;
using MediatR;

namespace Application.Features.EducationCQRS.Query.ListEducationWithSpecAsync;

public class ListEducationWithSpecQuery : IRequest<ResponseDTO<ICollection<EducationDTO>>>
{
    public ISpecification<Education> specification { get; set; }

    public ListEducationWithSpecQuery(ISpecification<Education> specification)
    {
        this.specification = specification;
    }
}
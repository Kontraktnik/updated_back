using Application.Contracts.Specification;
using Application.DTO.Common;
using Application.DTO.System;
using Domain.Models.SystemModels;
using MediatR;

namespace Application.Features.ArmyTypeCQRS.Query.ListArmyTypeWithSpecAsync;

public class ListArmyTypeWithSpecAsyncQuery : IRequest<ResponseDTO<ICollection<ArmyTypeDTO>>>
{
    public ISpecification<ArmyType> specification { get; set; }

    public ListArmyTypeWithSpecAsyncQuery(ISpecification<ArmyType> specification)
    {
        this.specification = specification;
    }
}
using Application.Contracts.Specification;
using Application.DTO.Common;
using Application.DTO.System;
using Domain.Models.SystemModels;
using MediatR;

namespace Application.Features.ArmyTypeCQRS.Query.GetArmyTypeWithSpecAsync;

public class GetArmyTypeWithSpecAsyncQuery : IRequest<ResponseDTO<ArmyTypeDTO>>
{
    public ISpecification<ArmyType> specification { get; set; }

    public GetArmyTypeWithSpecAsyncQuery(ISpecification<ArmyType> specification)
    {
        this.specification = specification;
    }
}
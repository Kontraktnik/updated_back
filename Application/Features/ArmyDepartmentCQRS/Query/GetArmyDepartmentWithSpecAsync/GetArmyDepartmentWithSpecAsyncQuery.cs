using Application.Contracts.Specification;
using Application.DTO.Common;
using Application.DTO.System;
using Domain.Models.SystemModels;
using MediatR;

namespace Application.Features.ArmyDepartmentCQRS.Query.GetArmyDepartmentWithSpecAsync;

public class GetArmyDepartmentWithSpecAsyncQuery : IRequest<ResponseDTO<ArmyDepartmentDTO>>
{
    public ISpecification<ArmyDepartment> specification { get; set; }

    public GetArmyDepartmentWithSpecAsyncQuery(ISpecification<ArmyDepartment> specification)
    {
        this.specification = specification;
    }
}
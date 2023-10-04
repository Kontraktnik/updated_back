using Application.Contracts.Specification;
using Application.DTO.Common;
using Application.DTO.System;
using Domain.Models.SystemModels;
using MediatR;

namespace Application.Features.ArmyDepartmentCQRS.Query.ListArmyDepartmentWithSpecAsync;

public class ListArmyDepartmentWithSpecAsyncQuery : IRequest<ResponseDTO<ICollection<ArmyDepartmentDTO>>>, IRequest<ICollection<ArmyDepartmentDTO>>
{
    public ISpecification<ArmyDepartment> specification { get; set; }

    public ListArmyDepartmentWithSpecAsyncQuery(ISpecification<ArmyDepartment> specification)
    {
        this.specification = specification;
    }
}
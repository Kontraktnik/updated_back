using Application.DTO.Common;
using Application.DTO.System;
using MediatR;

namespace Application.Features.ArmyDepartmentCQRS.Query.GetArmyDepartmentByIdAsync;

public class GetArmyDepartmentByIdAsync : IRequest<ResponseDTO<ArmyDepartmentDTO>>
{
    public long Id { get; set; }

    public GetArmyDepartmentByIdAsync(long Id)
    {
        this.Id = Id;
    }
}
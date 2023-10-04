using Application.DTO.Common;
using Application.DTO.System;
using MediatR;

namespace Application.Features.ArmyDepartmentCQRS.Query.ListArmyDepartmentAllAsync;

public class ListArmyDepartmentAllAsyncQuery : IRequest<ResponseDTO<ICollection<ArmyDepartmentDTO>>>
{
    
}
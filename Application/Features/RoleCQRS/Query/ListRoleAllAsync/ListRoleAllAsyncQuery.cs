using Application.DTO.Calculation;
using Application.DTO.Calculation.RankSalaryDTO;
using Application.DTO.Common;
using Application.DTO.System;
using Application.DTO.User;
using MediatR;

namespace Application.Features.RoleCQRS.Query.ListRoleAllAsync;

public class ListRoleAllAsyncQuery : IRequest<ResponseDTO<ICollection<RoleDTO>>>
{
    
}
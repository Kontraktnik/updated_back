using Application.Contracts.Specification;
using Application.DTO.Calculation;
using Application.DTO.Calculation.RankSalaryDTO;
using Application.DTO.Common;
using Application.DTO.System;
using Application.DTO.User;
using Domain.Models.CalculationModels;
using Domain.Models.SystemModels;
using MediatR;

namespace Application.Features.RoleCQRS.Query.GetRoleByIdAsync;

public class GetRoleByIdAsyncQuery : IRequest<ResponseDTO<RoleDTO>>
{
    public  long Id { get; set; }

    public GetRoleByIdAsyncQuery(long Id)
    {
        this.Id = Id;
    }
}
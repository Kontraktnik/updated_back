using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Calculation;
using Application.DTO.Calculation.RankSalaryDTO;
using Application.DTO.Common;
using Application.DTO.System;
using Application.DTO.User;
using Application.Features.ArmyTypeCQRS.Query.ListArmyTypeAllAsync;
using AutoMapper;
using Domain.Models.SystemModels;
using MediatR;

namespace Application.Features.RoleCQRS.Query.ListRoleAllAsync;

public class ListRoleAllAsyncQueryHandler : IRequestHandler<ListRoleAllAsyncQuery,ResponseDTO<ICollection<RoleDTO>>>
{
    private readonly IRoleRepository _repository;
    private readonly IMapper _mapper;

    public ListRoleAllAsyncQueryHandler(IRoleRepository repository,IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ResponseDTO<ICollection<RoleDTO>>> Handle(ListRoleAllAsyncQuery request, CancellationToken cancellationToken)
    {
        var models = await _repository.ListAllAsync();
        var modelsDTO = _mapper.Map<ICollection<RoleDTO>>(models);
        return new ResponseDTO<ICollection<RoleDTO>>
        {
            StatusCode = (int)HttpStatusCode.OK,
            Success = true,
            Data = modelsDTO,
        };
    }
}
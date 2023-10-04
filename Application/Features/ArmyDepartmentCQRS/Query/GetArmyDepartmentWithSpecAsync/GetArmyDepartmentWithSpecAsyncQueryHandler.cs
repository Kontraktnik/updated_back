using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Common;
using Application.DTO.System;
using Application.Resource;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Features.ArmyDepartmentCQRS.Query.GetArmyDepartmentWithSpecAsync;

public class GetArmyDepartmentWithSpecAsyncQueryHandler : IRequestHandler<GetArmyDepartmentWithSpecAsyncQuery,ResponseDTO<ArmyDepartmentDTO>>
{
    private readonly IArmyDepartmentRepository _armyDepartmentRepository;
    private readonly IMapper _mapper;
    private IStringLocalizer<Localize> localizer;

    public GetArmyDepartmentWithSpecAsyncQueryHandler(IArmyDepartmentRepository armyDepartmentRepository,IMapper mapper,IStringLocalizer<Localize> localizer)
    {
        _armyDepartmentRepository = armyDepartmentRepository;
        _mapper = mapper;
        this.localizer = localizer;
    }

    public async Task<ResponseDTO<ArmyDepartmentDTO>> Handle(GetArmyDepartmentWithSpecAsyncQuery request, CancellationToken cancellationToken)
    {
        var armyDepartment = await _armyDepartmentRepository.GetEntityWithSpecAsync(request.specification);
        if (armyDepartment != null)
        {
            return new ResponseDTO<ArmyDepartmentDTO>
            {
                StatusCode = (int)HttpStatusCode.OK,
                Success = true,
                Data = _mapper.Map<ArmyDepartmentDTO>(armyDepartment),
            };
        }
        else
        {
            return new ResponseDTO<ArmyDepartmentDTO>
            {
                StatusCode = (int)HttpStatusCode.NotFound,
                Success = false,
                Data = null,
                Message = this.localizer["NotFound"]
            };
        }
    }
}
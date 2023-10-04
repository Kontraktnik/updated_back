using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Common;
using Application.DTO.System;
using Application.Resource;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Features.ArmyDepartmentCQRS.Query.GetArmyDepartmentByIdAsync;

public class GetArmyDepartmentByIdAsyncQueryHandler : IRequestHandler<GetArmyDepartmentByIdAsync,ResponseDTO<ArmyDepartmentDTO>>
{
    private readonly IArmyDepartmentRepository _armyDepartmentRepository;
    private readonly IMapper _mapper;
    private IStringLocalizer<Localize> localizer;

    public GetArmyDepartmentByIdAsyncQueryHandler(IArmyDepartmentRepository armyDepartmentRepository,IMapper mapper,IStringLocalizer<Localize> _localizer)
    {
        _armyDepartmentRepository = armyDepartmentRepository;
        _mapper = mapper;
        localizer = _localizer;
    }
    public async Task<ResponseDTO<ArmyDepartmentDTO>> Handle(GetArmyDepartmentByIdAsync request, CancellationToken cancellationToken)
    {
        var armyDepartment = await _armyDepartmentRepository.GetByIdAsync(request.Id);
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
                Message = localizer["NotFound"]
            };
        }
    }
}
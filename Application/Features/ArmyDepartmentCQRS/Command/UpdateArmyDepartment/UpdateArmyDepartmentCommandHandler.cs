using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Common;
using Application.DTO.System;
using Application.Resource;
using AutoMapper;
using Domain.Models.SystemModels;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Features.ArmyDepartmentCQRS.Command.UpdateArmyDepartment;

public class UpdateArmyDepartmentCommandHandler : IRequestHandler<UpdateArmyDepartmentCommand,ResponseDTO<ArmyDepartmentDTO>>
{
    private readonly IArmyDepartmentRepository _armyDepartmentRepository;
    private readonly IMapper _mapper;
    private IStringLocalizer<Localize> localizer;

    public UpdateArmyDepartmentCommandHandler(IArmyDepartmentRepository armyDepartmentRepository,IMapper mapper, IStringLocalizer<Localize> localizer)
    {
        _armyDepartmentRepository = armyDepartmentRepository;
        _mapper = mapper;
        this.localizer = localizer;
    }

    public async Task<ResponseDTO<ArmyDepartmentDTO>> Handle(UpdateArmyDepartmentCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if ((await _armyDepartmentRepository.GetByIdAsync(request.Id)) != null)
            {
                var updatedArmyDepartment = _mapper.Map<ArmyDepartment>(request.model);
                var armyDepartment = await _armyDepartmentRepository.UpdateAsync(updatedArmyDepartment);
                return new ResponseDTO<ArmyDepartmentDTO>
                {
                    Success = true,
                    StatusCode = (int) HttpStatusCode.Created,
                    Message = localizer["Updated"],
                    Data = _mapper.Map<ArmyDepartmentDTO>(armyDepartment)
                };  
            }
            return new ResponseDTO<ArmyDepartmentDTO>
            {
                Success = false,
                StatusCode = (int) HttpStatusCode.NotFound,
                Message = localizer["NotFound"],
            };  
        }
        catch (Exception ex)
        {
            return new ResponseDTO<ArmyDepartmentDTO>
            {
                Success = false,
                StatusCode = (int) HttpStatusCode.InternalServerError,
                Message = this.localizer["UnexpectedError"] +":" + ex.Message,
            };
        }
    }
}
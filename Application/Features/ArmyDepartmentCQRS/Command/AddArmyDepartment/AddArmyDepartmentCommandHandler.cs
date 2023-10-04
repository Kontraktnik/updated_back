using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Common;
using Application.DTO.System;
using Application.Resource;
using AutoMapper;
using Domain.Models.SystemModels;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Features.ArmyDepartmentCQRS.Command.AddArmyDepartment;

public class AddArmyDepartmentCommandHandler : IRequestHandler<AddArmyDepartmentCommand,ResponseDTO<ArmyDepartmentDTO>>
{
    private readonly IArmyDepartmentRepository _armyDepartmentRepository;
    private readonly IMapper _mapper;
    private IStringLocalizer<Localize> localizer;

    public AddArmyDepartmentCommandHandler(IArmyDepartmentRepository armyDepartmentRepository,IMapper mapper,
        IStringLocalizer<Localize> localizer)
    {
        _armyDepartmentRepository = armyDepartmentRepository;
        _mapper = mapper;
        this.localizer = localizer;
    }


    public async Task<ResponseDTO<ArmyDepartmentDTO>> Handle(AddArmyDepartmentCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var newArmyDepartment = _mapper.Map<ArmyDepartment>(request.model);
            var armyDepartment = await _armyDepartmentRepository.AddAsync(newArmyDepartment);
             return new ResponseDTO<ArmyDepartmentDTO>
            {
                Success = true,
                StatusCode = (int) HttpStatusCode.Created,
                Message = localizer["Created"],
                Data = _mapper.Map<ArmyDepartmentDTO>(armyDepartment)
            };
        }
        catch (Exception ex)
        {
            return new ResponseDTO<ArmyDepartmentDTO>
            {
                Success = false,
                StatusCode = (int) HttpStatusCode.InternalServerError,
                Message = this.localizer["UnexpectedError"] +":" + ex.Message
            };
        }
    }
}
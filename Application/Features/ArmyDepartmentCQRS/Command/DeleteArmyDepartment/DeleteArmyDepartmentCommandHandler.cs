using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Common;
using Application.DTO.System;
using Application.Resource;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Features.ArmyDepartmentCQRS.Command.DeleteArmyDepartment;

public class DeleteArmyDepartmentCommandHandler : IRequestHandler<DeleteArmyDepartmentCommand,ResponseDTO<bool>>
{
    private readonly IArmyDepartmentRepository _armyDepartmentRepository;
    private readonly IMapper _mapper;
    private IStringLocalizer<Localize> localizer;

    public DeleteArmyDepartmentCommandHandler(IArmyDepartmentRepository armyDepartmentRepository,IMapper mapper,IStringLocalizer<Localize> localizer)
    {
        _armyDepartmentRepository = armyDepartmentRepository;
        _mapper = mapper;
        this.localizer = localizer;
    }

    public async Task<ResponseDTO<bool>> Handle(DeleteArmyDepartmentCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var deletedArmyDepartment = await _armyDepartmentRepository.GetByIdAsync(request.Id);
            if (deletedArmyDepartment != null)
            {
                var armyDepartment = await _armyDepartmentRepository.DeleteAsync(deletedArmyDepartment);
                return new ResponseDTO<bool>
                {
                    Success = true,
                    StatusCode = (int) HttpStatusCode.OK,
                    Message = localizer["Deleted"],
                    Data = true
                };  
            }
            return new ResponseDTO<bool>
            {
                Success = false,
                StatusCode = (int) HttpStatusCode.NotFound,
                Message = localizer["NotFound"],
                Data = false
            }; 
        }
        catch (Exception ex)
        {
            return new ResponseDTO<bool>
            {
                Success = false,
                StatusCode = (int) HttpStatusCode.InternalServerError,
                Message = this.localizer["UnexpectedError"] +":" + ex.Message,
                Data = false
            }; 
        }
    }
}
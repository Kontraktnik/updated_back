using System.Net;
using API.Helpers;
using Application.DTO.Common;
using Application.DTO.System;
using Application.Features.ArmyDepartmentCQRS.Command.AddArmyDepartment;
using Application.Features.ArmyDepartmentCQRS.Command.DeleteArmyDepartment;
using Application.Features.ArmyDepartmentCQRS.Command.UpdateArmyDepartment;
using Application.Features.ArmyDepartmentCQRS.Query.GetArmyDepartmentByIdAsync;
using Application.Features.ArmyDepartmentCQRS.Query.GetArmyDepartmentWithSpecAsync;
using Application.Features.ArmyDepartmentCQRS.Query.ListArmyDepartmentAllAsync;
using Application.Features.ArmyDepartmentCQRS.Query.ListArmyDepartmentWithSpecAsync;
using Infrastracture.Contracts.Parameters.ArmyDepartmentParameters;
using Infrastracture.Contracts.Specifications.ArmyDepartmentSpecification;
using Infrastracture.Helpers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.SystemController;

public class ArmyDepartmentController : BaseApiController
{
    private readonly IMediator _mediator;

    public ArmyDepartmentController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }
    
    [HttpGet]
    [ProducesResponseType(typeof(ResponseDTO<ArmyDepartmentDTO>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ArmyDepartmentDTO>> GetById([FromQuery] long Id)
    {
        var query = new GetArmyDepartmentByIdAsync(Id);
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);

    }
    [HttpGet]
    [ProducesResponseType(typeof(ResponseDTO<ArmyDepartmentDTO>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ArmyDepartmentDTO>> Get([FromQuery] ArmyDepartmentParameter parameter)
    {
        var specification = new ArmyDepartmentSpecification(parameter);
        var query = new GetArmyDepartmentWithSpecAsyncQuery(specification);
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);

    }
    [HttpGet]
    [ProducesResponseType(typeof(ResponseDTO<ICollection<ArmyDepartmentDTO>>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ResponseDTO<ICollection<ArmyDepartmentDTO>>>> GetAll()
    {
        var query = new ListArmyDepartmentAllAsyncQuery();
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);

    }
    [HttpGet]
    [ProducesResponseType(typeof(ResponseDTO<ICollection<ArmyDepartmentDTO>>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ResponseDTO<ICollection<ArmyDepartmentDTO>>>> All([FromQuery] ArmyDepartmentParameter parameter)
    {
        var specification = new ArmyDepartmentSpecification(parameter);
        var query = new ListArmyDepartmentWithSpecAsyncQuery(specification);
        var result =  await _mediator.Send(query);
        return Ok(result);
    }
    
    [AuthorizeByRole(AppConstant.AdminRoleName)]
    [HttpPost]
    [ProducesResponseType(typeof(ResponseDTO<ArmyDepartmentDTO>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ResponseDTO<ArmyDepartmentDTO>>> Create([FromBody] ArmyDepartmentDTO armyDepartmentDto)
    {
        var query = new AddArmyDepartmentCommand(armyDepartmentDto);
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);

    }
   
    [AuthorizeByRole(AppConstant.AdminRoleName)]
    [HttpPut]
    [ProducesResponseType(typeof(ResponseDTO<ArmyDepartmentDTO>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ResponseDTO<ArmyDepartmentDTO>>> Update([FromBody] ArmyDepartmentDTO armyDepartmentDto,long Id)
    {
        var query = new UpdateArmyDepartmentCommand(Id,armyDepartmentDto);
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);

    }
    
    [AuthorizeByRole(AppConstant.AdminRoleName)]
    [HttpDelete]
    [ProducesResponseType(typeof(ResponseDTO<bool>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ResponseDTO<bool>>> Delete(long Id)
    {
        var query = new DeleteArmyDepartmentCommand(Id);
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);

    }
    
}
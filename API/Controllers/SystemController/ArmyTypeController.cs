using System.Net;
using API.Helpers;
using Application.DTO.Common;
using Application.DTO.System;
using Application.Features.ArmyTypeCQRS.Command.AddArmyType;
using Application.Features.ArmyTypeCQRS.Command.DeleteArmyType;
using Application.Features.ArmyTypeCQRS.Command.UpdateArmyType;
using Application.Features.ArmyTypeCQRS.Query.GetArmyTypeByIdAsync;
using Application.Features.ArmyTypeCQRS.Query.GetArmyTypeWithSpecAsync;
using Application.Features.ArmyTypeCQRS.Query.ListArmyTypeAllAsync;
using Application.Features.ArmyTypeCQRS.Query.ListArmyTypeWithSpecAsync;
using Infrastracture.Contracts.Parameters.ArmyTypeParameters;
using Infrastracture.Contracts.Specifications.ArmyTypeSpecification;
using Infrastracture.Helpers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.SystemController;

public class ArmyTypeController : BaseApiController
{
    private readonly IMediator _mediator;

    public ArmyTypeController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }
    
    [HttpGet]
    [ProducesResponseType(typeof(ResponseDTO<ArmyTypeDTO>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ArmyTypeDTO>> GetById([FromQuery] long Id)
    {
        var query = new GetArmyTypeByIdAsyncQuery(Id);
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);

    }
    [HttpGet]
    [ProducesResponseType(typeof(ResponseDTO<ArmyTypeDTO>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ArmyTypeDTO>> Get([FromQuery] ArmyTypeParameter parameter)
    {
        var specification = new ArmyTypeSpecification(parameter);
        var query = new GetArmyTypeWithSpecAsyncQuery(specification);
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);

    }
    [HttpGet]
    [ProducesResponseType(typeof(ResponseDTO<ICollection<ArmyTypeDTO>>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ResponseDTO<ICollection<ArmyTypeDTO>>>> GetAll()
    {
        var query = new ListArmyTypeAllAsyncQuery();
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);

    }
    [HttpGet]
    [ProducesResponseType(typeof(ResponseDTO<ICollection<ArmyTypeDTO>>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ResponseDTO<ICollection<ArmyTypeDTO>>>> All([FromQuery] ArmyTypeParameter parameter)
    {
        var specification = new ArmyTypeSpecification(parameter);
        var query = new ListArmyTypeWithSpecAsyncQuery(specification);
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);

    }
    
    [AuthorizeByRole(AppConstant.AdminRoleName)]
    [HttpPost]
    [ProducesResponseType(typeof(ResponseDTO<ArmyTypeDTO>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ResponseDTO<ArmyTypeDTO>>> Create([FromBody] ArmyTypeDTO model)
    {
        var query = new AddArmyTypeCommand(model);
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);

    }
   
    [AuthorizeByRole(AppConstant.AdminRoleName)]
    [HttpPut]
    [ProducesResponseType(typeof(ResponseDTO<ArmyTypeDTO>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ResponseDTO<ArmyTypeDTO>>> Update([FromBody] ArmyTypeDTO model,long Id)
    {
        var query = new UpdateArmyTypeCommand(model,Id);
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);

    }
    
    [AuthorizeByRole(AppConstant.AdminRoleName)]
    [HttpDelete]
    [ProducesResponseType(typeof(ResponseDTO<bool>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ResponseDTO<bool>>> Delete(long Id)
    {
        var query = new DeleteArmyTypeCommand(Id);
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);

    }
}
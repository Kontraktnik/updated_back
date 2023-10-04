using System.Net;
using API.Helpers;
using Application.DTO.Common;
using Application.DTO.System;
using Application.Features.ArmyRegionCQRS.Command.AddArmyRegion;
using Application.Features.ArmyRegionCQRS.Command.DeleteArmyRegion;
using Application.Features.ArmyRegionCQRS.Command.UpdateArmyRegion;
using Application.Features.ArmyRegionCQRS.Query.GetArmyRegionByIdAsync;
using Application.Features.ArmyRegionCQRS.Query.GetArmyRegionWithSpecAsync;
using Application.Features.ArmyRegionCQRS.Query.ListArmyRegionAllAsync;
using Application.Features.ArmyRegionCQRS.Query.ListArmyRegionWithSpecAsync;
using Infrastracture.Contracts.Parameters.ArmyRegionParameters;
using Infrastracture.Contracts.Specifications.ArmyRegionSpecification;
using Infrastracture.Helpers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.SystemController;

public class ArmyRegionController : BaseApiController
{
    private readonly IMediator _mediator;

    public ArmyRegionController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }
    
    [HttpGet]
    [ProducesResponseType(typeof(ResponseDTO<ArmyRegionDTO>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ArmyRegionDTO>> GetById([FromQuery] long Id)
    {
        var query = new GetArmyRegionByIdAsyncQuery(Id);
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);

    }
    [HttpGet]
    [ProducesResponseType(typeof(ResponseDTO<ArmyRankDTO>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ArmyRegionDTO>> Get([FromQuery] ArmyRegionParameter parameter)
    {
        var specification = new ArmyRegionSpecification(parameter);
        var query = new GetArmyRegionWithSpecAsyncQuery(specification);
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);

    }
    [HttpGet]
    [ProducesResponseType(typeof(ResponseDTO<ICollection<ArmyRegionDTO>>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ResponseDTO<ICollection<ArmyRegionDTO>>>> GetAll()
    {
        var query = new ListArmyRegionAllAsyncQuery();
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);

    }
    [HttpGet]
    [ProducesResponseType(typeof(ResponseDTO<ICollection<ArmyRegionDTO>>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ResponseDTO<ICollection<ArmyRegionDTO>>>> All([FromQuery] ArmyRegionParameter parameter)
    {
        var specification = new ArmyRegionSpecification(parameter);
        var query = new ListArmyRegionWithSpecAsyncQuery(specification);
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);

    }
    
    [AuthorizeByRole(AppConstant.AdminRoleName)]
    [HttpPost]
    [ProducesResponseType(typeof(ResponseDTO<ArmyRegionDTO>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ResponseDTO<ArmyRegionDTO>>> Create([FromBody] ArmyRegionDTO model)
    {
        var query = new AddArmyRegionCommand(model);
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);

    }
   
    [AuthorizeByRole(AppConstant.AdminRoleName)]
    [HttpPut]
    [ProducesResponseType(typeof(ResponseDTO<ArmyRegionDTO>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ResponseDTO<ArmyRegionDTO>>> Update([FromBody] ArmyRegionDTO model,long Id)
    {
        var query = new UpdateArmyRegionCommand(model,Id);
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);

    }
    
    [AuthorizeByRole(AppConstant.AdminRoleName)]
    [HttpDelete]
    [ProducesResponseType(typeof(ResponseDTO<bool>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ResponseDTO<bool>>> Delete(long Id)
    {
        var query = new DeleteArmyRegionCommand(Id);
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);
    }
    
}
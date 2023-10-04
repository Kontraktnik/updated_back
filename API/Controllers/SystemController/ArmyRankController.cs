using System.Net;
using API.Helpers;
using Application.DTO.Common;
using Application.DTO.System;
using Application.Features.AreaCQRS.Query.GetAreaByIdAsync;
using Application.Features.ArmyRankCQRS.Command.AddArmyRank;
using Application.Features.ArmyRankCQRS.Command.DeleteArmyRank;
using Application.Features.ArmyRankCQRS.Command.UpdateArmyRank;
using Application.Features.ArmyRankCQRS.Query.GetArmyRankByIdAsync;
using Application.Features.ArmyRankCQRS.Query.GetArmyRankWithSpecAsync;
using Application.Features.ArmyRankCQRS.Query.ListArmyRankAllAsync;
using Application.Features.ArmyRankCQRS.Query.ListArmyRankWithSpecAsync;
using Infrastracture.Contracts.Parameters.ArmyRankParameters;
using Infrastracture.Contracts.Specifications.ArmyRankSpecification;
using Infrastracture.Helpers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.SystemController;

public class ArmyRankController : BaseApiController
{
    private readonly IMediator _mediator;

    public ArmyRankController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }
    
    [HttpGet]
    [ProducesResponseType(typeof(ResponseDTO<ArmyRankDTO>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ArmyRankDTO>> GetById([FromQuery] long Id)
    {
        var query = new GetArmyRankByIdAsyncQuery(Id);
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);

    }
    [HttpGet]
    [ProducesResponseType(typeof(ResponseDTO<ArmyRankDTO>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ArmyRankDTO>> Get([FromQuery] ArmyRankParameter parameter)
    {
        var specification = new ArmyRankSpecification(parameter);
        var query = new GetArmyRankWithSpecAsyncQuery(specification);
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);

    }
    [HttpGet]
    [ProducesResponseType(typeof(ResponseDTO<ICollection<ArmyRankDTO>>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ResponseDTO<ICollection<ArmyRankDTO>>>> GetAll()
    {
        var query = new ListArmyRankAllAsyncQuery();
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);

    }
    [HttpGet]
    [ProducesResponseType(typeof(ResponseDTO<ICollection<ArmyRankDTO>>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ResponseDTO<ICollection<ArmyRankDTO>>>> All([FromQuery] ArmyRankParameter parameter)
    {
        var specification = new ArmyRankSpecification(parameter);
        var query = new ListArmyRankWithSpecAsyncQuery(specification);
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);

    }
    
    [AuthorizeByRole(AppConstant.AdminRoleName)]
    [HttpPost]
    [ProducesResponseType(typeof(ResponseDTO<ArmyRankDTO>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ResponseDTO<ArmyRankDTO>>> Create([FromBody] ArmyRankDTO model)
    {
        var query = new AddArmyRankCommand(model);
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);

    }
   
    [AuthorizeByRole(AppConstant.AdminRoleName)]
    [HttpPut]
    [ProducesResponseType(typeof(ResponseDTO<ArmyRankDTO>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ResponseDTO<ArmyRankDTO>>> Update([FromBody] ArmyRankDTO model,long Id)
    {
        var query = new UpdateArmyRankCommand(Id,model);
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);

    }
    
    [AuthorizeByRole(AppConstant.AdminRoleName)]
    [HttpDelete]
    [ProducesResponseType(typeof(ResponseDTO<bool>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ResponseDTO<bool>>> Delete(long Id)
    {
        var query = new DeleteArmyRankCommand(Id);
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);

    }
    
    
    
    
    
}
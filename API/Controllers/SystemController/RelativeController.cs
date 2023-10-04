using System.Net;
using API.Helpers;
using Application.DTO.Common;
using Application.DTO.System;
using Application.Features.RelativeCQRS.Command.AddRelative;
using Application.Features.RelativeCQRS.Command.DeleteRelative;
using Application.Features.RelativeCQRS.Command.UpdateRelative;
using Application.Features.RelativeCQRS.Query.GetRelativeByIdAsync;
using Application.Features.RelativeCQRS.Query.GetRelativeWithSpecAsync;
using Application.Features.RelativeCQRS.Query.ListRelativeAllAsync;
using Application.Features.RelativeCQRS.Query.ListRelativeWithSpecAsync;
using Infrastracture.Contracts.Parameters.RelativeParameters;
using Infrastracture.Contracts.Specifications.RelativeSpecification;
using Infrastracture.Helpers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.SystemController;

public class RelativeController : BaseApiController
{
    private readonly IMediator _mediator;

    public RelativeController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }
    
    [HttpGet]
    [ProducesResponseType(typeof(ResponseDTO<RelativeDTO>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<RelativeDTO>> GetById([FromQuery] long Id)
    {
        var query = new GetRelativeByIdAsyncQuery(Id);
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);

    }
    
    
    
    [HttpGet]
    [ProducesResponseType(typeof(ResponseDTO<RelativeDTO>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<RelativeDTO>> Get([FromQuery] RelativeParameter parameter)
    {
        var specification = new RelativeSpecification(parameter);
        var query = new GetRelativeWithSpecAsyncQuery(specification);
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);

    }
    [HttpGet]
    [ProducesResponseType(typeof(ResponseDTO<ICollection<RelativeDTO>>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ResponseDTO<ICollection<RelativeDTO>>>> GetAll()
    {
        var query = new ListRelativeAllAsyncQuery();
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);

    }
    [HttpGet]
    [ProducesResponseType(typeof(ResponseDTO<ICollection<RelativeDTO>>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ResponseDTO<ICollection<RelativeDTO>>>> All([FromQuery] RelativeParameter parameter)
    {
        var specification = new RelativeSpecification(parameter);
        var query = new ListRelativeWithSpecAsyncQuery(specification);
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);

    }
    
    [AuthorizeByRole(AppConstant.AdminRoleName)]
    [HttpPost]
    [ProducesResponseType(typeof(ResponseDTO<RelativeDTO>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ResponseDTO<RelativeDTO>>> Create([FromBody] RelativeDTO model)
    {
        var query = new AddRelativeCommand(model);
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);

    }
   
    [AuthorizeByRole(AppConstant.AdminRoleName)]
    [HttpPut]
    [ProducesResponseType(typeof(ResponseDTO<RelativeDTO>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ResponseDTO<RelativeDTO>>> Update([FromBody] RelativeDTO model,long Id)
    {
        var query = new UpdateRelativeCommand(model,Id);
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);

    }
    
    [AuthorizeByRole(AppConstant.AdminRoleName)]
    [HttpDelete]
    [ProducesResponseType(typeof(ResponseDTO<bool>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ResponseDTO<bool>>> Delete(long Id)
    {
        var query = new DeleteRelativeCommand(Id);
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);

    }
}
using System.Net;
using API.Helpers;
using Application.DTO.Common;
using Application.DTO.System;
using Application.Features.VTShCQRS.Command.AddVTSh;
using Application.Features.VTShCQRS.Command.DeleteVTSh;
using Application.Features.VTShCQRS.Command.UpdateVTSh;
using Application.Features.VTShCQRS.Query.GetVTShByIdAsync;
using Application.Features.VTShCQRS.Query.GetVTShWithSpecAsync;
using Application.Features.VTShCQRS.Query.ListVTShAllAsync;
using Application.Features.VTShCQRS.Query.ListVTShWithSpecAsync;
using Infrastracture.Contracts.Parameters.VTShParameters;
using Infrastracture.Contracts.Specifications.VTShSpecification;
using Infrastracture.Helpers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.SystemController;

public class VTShController : BaseApiController
{
    private readonly IMediator _mediator;

    public VTShController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }
    
    [HttpGet]
    [ProducesResponseType(typeof(ResponseDTO<VTShDTO>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<VTShDTO>> GetById([FromQuery] long Id)
    {
        var query = new GetVTShByIdAsyncQuery(Id);
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);

    }
    [HttpGet]
    [ProducesResponseType(typeof(ResponseDTO<VTShDTO>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<VTShDTO>> Get([FromQuery] VTShParameter parameter)
    {
        var specification = new VTShSpecification(parameter);
        var query = new GetVTShWithSpecAsyncQuery(specification);
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);

    }
    [HttpGet]
    [ProducesResponseType(typeof(ResponseDTO<ICollection<VTShDTO>>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ResponseDTO<ICollection<VTShDTO>>>> GetAll()
    {
        var query = new ListVTShAllAsyncQuery();
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);

    }
    [HttpGet]
    [ProducesResponseType(typeof(ResponseDTO<ICollection<VTShDTO>>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ResponseDTO<ICollection<VTShDTO>>>> All([FromQuery] VTShParameter parameter)
    {
        var specification = new VTShSpecification(parameter);
        var query = new ListVTShWithSpecAsyncQuery(specification);
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);

    }
    
    [AuthorizeByRole(AppConstant.AdminRoleName)]
    [HttpPost]
    [ProducesResponseType(typeof(ResponseDTO<VTShDTO>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ResponseDTO<VTShDTO>>> Create([FromBody] VTShDTO model)
    {
        var query = new AddVTShCommand(model);
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);

    }
   
    [AuthorizeByRole(AppConstant.AdminRoleName)]
    [HttpPut]
    [ProducesResponseType(typeof(ResponseDTO<VTShDTO>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ResponseDTO<VTShDTO>>> Update([FromBody] VTShDTO model,long Id)
    {
        var query = new UpdateVTShCommand(model,Id);
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);

    }
    
    [AuthorizeByRole(AppConstant.AdminRoleName)]
    [HttpDelete]
    [ProducesResponseType(typeof(ResponseDTO<bool>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ResponseDTO<bool>>> Delete(long Id)
    {
        var query = new DeleteVTShCommand(Id);
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);

    }
}
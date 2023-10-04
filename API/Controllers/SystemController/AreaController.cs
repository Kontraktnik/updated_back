using System.Net;
using API.Helpers;
using API.Model;
using Application.DTO.Common;
using Application.DTO.System;
using Application.Features.AreaCQRS.Command.AddArea;
using Application.Features.AreaCQRS.Command.DeleteArea;
using Application.Features.AreaCQRS.Command.UpdateArea;
using Application.Features.AreaCQRS.Query.CountAreaAsync;
using Application.Features.AreaCQRS.Query.GetAreaByIdAsync;
using Application.Features.AreaCQRS.Query.GetAreaWithSpecAsync;
using Application.Features.AreaCQRS.Query.ListAreaAllAsync;
using Application.Features.AreaCQRS.Query.ListAreaWithSpecAsync;
using Infrastracture.Contracts.Parameters.AreaParameters;
using Infrastracture.Contracts.Specifications.AreaSpecification;
using Infrastracture.Helpers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.SystemController;

public class AreaController : BaseApiController
{
    private readonly IMediator _mediator;

    public AreaController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }
    
    [HttpGet]
    [ProducesResponseType(typeof(ResponseDTO<AreaDTO>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ResponseDTO<AreaDTO>>> GetById(long Id)
    {
        var query = new GetAreaByIdAsyncQuery(Id);
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode, result);
    }
    
    [HttpGet]
    [ProducesResponseType(typeof(ResponseDTO<ICollection<AreaDTO>>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ResponseDTO<ICollection<AreaDTO>>>> All()
    {
        var query = new ListAreaAllAsyncQuery();
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode, result);
    }

    [HttpGet]
    [ProducesResponseType(typeof(Pagination<ICollection<AreaDTO>>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<Pagination<ICollection<AreaDTO>>>> GetAll([FromQuery] AreaParameter areaParameter)
    {
        var specification = new AreaSpecification(areaParameter);
        var query = new ListAreaWithSpecAsyncQuery(specification);
        var listOfAreas =  await _mediator.Send(query);
        var countQuery =  new CountAreaAsyncQuery(specification);
        int total =   await _mediator.Send(countQuery);
        var pagination = new Pagination<AreaDTO>(pageIndex: areaParameter.PageIndex, pageSize: areaParameter.PageSize,
            total, listOfAreas.Data);
        return Ok(pagination);
    }
    
    [HttpGet]
    [ProducesResponseType(typeof(ResponseDTO<AreaDTO>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ResponseDTO<AreaDTO>>> Get([FromQuery] AreaParameter areaParameter)
    {
        var specification = new AreaSpecification(areaParameter);
        var query = new GetAreaWithSpecAsyncQuery(specification);
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode, result);
    }
    
    
    //Admin Resource
    
    [AuthorizeByRole(AppConstant.AdminRoleName)]
    [HttpPost]
    [ProducesResponseType(typeof(ResponseDTO<AreaDTO>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ResponseDTO<AreaDTO>>> Create([FromBody] AreaDTO model)
    {
        var query = new AddAreaCommand(model);
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode, result);
    }
   
    [AuthorizeByRole(AppConstant.AdminRoleName)]
    [HttpPut]
    [ProducesResponseType(typeof(ResponseDTO<AreaDTO>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ResponseDTO<AreaDTO>>> Update([FromBody] AreaDTO model,long Id)
    {
        var query = new UpdateAreaCommand(model,Id);
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode, result);
    }
    
    [AuthorizeByRole(AppConstant.AdminRoleName)]
    [HttpDelete]
    [ProducesResponseType(typeof(ResponseDTO<bool>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ResponseDTO<bool>>> Delete([FromQuery] long Id)
    {
        var query = new DeleteAreaCommand(Id);
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode, result);

    }
    
    
    
}
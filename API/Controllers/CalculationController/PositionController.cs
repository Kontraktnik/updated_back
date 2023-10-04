using System.Net;
using API.Helpers;
using API.Model;
using Application.DTO.Calculation.PositionDTO;
using Application.DTO.Common;
using Application.Features.PositionCQRS.Command.AddPosition;
using Application.Features.PositionCQRS.Command.DeletePosition;
using Application.Features.PositionCQRS.Command.UpdatePosition;
using Application.Features.PositionCQRS.Query.CountPositionAsync;
using Application.Features.PositionCQRS.Query.GetPositionByIdAsync;
using Application.Features.PositionCQRS.Query.GetPositionWithSpecAsync;
using Application.Features.PositionCQRS.Query.ListPositionAllAsync;
using Application.Features.PositionCQRS.Query.ListPositionWithSpecAsync;
using Infrastracture.Contracts.Parameters.PositionParameters;
using Infrastracture.Contracts.Specifications.PositionSpecification;
using Infrastracture.Helpers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.CalculationController;

public class PositionController : BaseApiController
{
    private readonly IMediator _mediator;

    public PositionController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }
    
    [HttpGet]
    [ProducesResponseType(typeof(ResponseDTO<PositionRDTO>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<PositionRDTO>> GetById([FromQuery] long Id)
    {
        var specification = new PositionSpecification(Id,null);
        var query = new GetPositionByIdAsyncQuery(specification);
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);

    }
    [HttpGet]
    [ProducesResponseType(typeof(ResponseDTO<PositionRDTO>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<PositionRDTO>> Get([FromQuery] PositionParameter parameter)
    {
        var specification = new PositionSpecification(parameter);
        var query = new GetPositionWithSpecAsyncQuery(specification);
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);

    }
    [HttpGet]
    [ProducesResponseType(typeof(ResponseDTO<ICollection<PositionRDTO>>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ResponseDTO<ICollection<PositionRDTO>>>> GetAll()
    {
        var query = new ListPositionAllAsyncQuery();
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);

    }
    [HttpGet]
    [ProducesResponseType(typeof(ResponseDTO<Pagination<PositionRDTO>>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<Pagination<PositionRDTO>>> All([FromQuery] PositionParameter parameter)
    {
        var specification = new PositionSpecification(parameter);
        var query = new ListPositionWithSpecAsyncQuery(specification);
        var result =  await _mediator.Send(query);
        var pagination = new Pagination<PositionRDTO>(
            parameter.PageIndex,
            parameter.PageSize,
            (await _mediator.Send(new CountPositionAsyncQuery(new PositionSpecification(parameter,false)))),
            result.Data
            );
        return StatusCode(result.StatusCode,pagination);

    }
    
    [AuthorizeByRole(AppConstant.AdminRoleName)]
    [HttpPost]
    [ProducesResponseType(typeof(ResponseDTO<PositionRDTO>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ResponseDTO<PositionRDTO>>> Create([FromBody] PositionCUDTO model)
    {
        var query = new AddPositionCommand(model);
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);

    }
   
    [AuthorizeByRole(AppConstant.AdminRoleName)]
    [HttpPut]
    [ProducesResponseType(typeof(ResponseDTO<PositionRDTO>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ResponseDTO<PositionRDTO>>> Update([FromBody] PositionCUDTO model,long Id)
    {
        var query = new UpdatePositionCommand(model,Id);
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);

    }
    
    [AuthorizeByRole(AppConstant.AdminRoleName)]
    [HttpDelete]
    [ProducesResponseType(typeof(ResponseDTO<bool>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ResponseDTO<bool>>> Delete(long Id)
    {
        var query = new DeletePositionCommand(Id);
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);

    }
}
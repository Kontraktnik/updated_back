using System.Net;
using API.Helpers;
using Application.DTO.Calculation;
using Application.DTO.Common;
using Application.DTO.Step;
using Application.Features.QualificationCQRS.Command.UpdateQualification;
using Application.Features.StepGroupCQRS.Command.UpdateStepGroup;
using Application.Features.StepGroupCQRS.Query.GetStepGroupByIdAsync;
using Application.Features.StepGroupCQRS.Query.GetStepGroupWithSpecAsync;
using Application.Features.StepGroupCQRS.Query.ListStepGroupWithSpecAsync;
using Infrastracture.Contracts.Parameters.StepGroupParameters;
using Infrastracture.Contracts.Specifications.StepGroupSpecification;
using Infrastracture.Helpers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.StepController;

public class StepGroupController : BaseApiController
{
    private readonly IMediator _mediator;

    public StepGroupController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    [ProducesResponseType(typeof(ResponseDTO<StepGroupDTO>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<StepGroupDTO>> GetById([FromQuery] long Id)
    {
        var query = new GetStepGroupByIdAsyncQuery(Id);
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);

    }
    
    [HttpGet]
    [ProducesResponseType(typeof(ResponseDTO<StepGroupDTO>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<StepGroupDTO>> Get([FromQuery] long Id)
    {
        var specification = new StepGroupSpecification(Id,null);
        var query = new GetStepGroupWithSpecAsyncQuery(specification);
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);

    }
    
    [HttpGet]
    [ProducesResponseType(typeof(ResponseDTO<StepGroupDTO>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<StepGroupDTO>> GetWithSpec([FromQuery] StepGroupParameter parameter)
    {
        var specification = new StepGroupSpecification(parameter);
        var query = new GetStepGroupWithSpecAsyncQuery(specification);
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);

    }
    
    [HttpGet]
    [ProducesResponseType(typeof(ResponseDTO<ICollection<StepGroupDTO>>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ICollection<StepGroupDTO>>> GetAll([FromQuery] StepGroupParameter parameter)
    {
        var specification = new StepGroupSpecification(parameter);
        var query = new ListStepGroupWithSpecAsyncQuery(specification);
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);

    }
    
    [AuthorizeByRole(AppConstant.AdminRoleName)]
    [HttpPut]
    [ProducesResponseType(typeof(ResponseDTO<StepGroupDTO>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ResponseDTO<StepGroupDTO>>> Update([FromBody] StepGroupDTO model,long Id)
    {
        var query = new UpdateStepGroupCommand(model,Id);
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);

    }
    
    
}
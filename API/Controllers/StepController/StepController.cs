using System.Net;
using API.Helpers;
using Application.DTO.Calculation;
using Application.DTO.Common;
using Application.DTO.Step;
using Application.Features.SecretLevelCQRS.Command.UpdateSecretLevel;
using Application.Features.StepCQRS.Command.UpdateStep;
using Application.Features.StepCQRS.Query.GetStepByIdAsync;
using Application.Features.StepCQRS.Query.ListStepAllWithSpecAsync;
using Infrastracture.Contracts.Specifications.StepSpecification;
using Infrastracture.Helpers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.StepController;

public class StepController : BaseApiController
{
    private readonly IMediator _mediator;

    public StepController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [Authorize]
    [HttpGet]
    [ProducesResponseType(typeof(ResponseDTO<StepDTO>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ResponseDTO<StepDTO>>> GetById([FromQuery] long Id)
    {
        var specification = new StepSpecification(Id);
        var query = new GetStepByIdAsyncQuery(specification);
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);

    }
    
    [Authorize]
    [HttpGet]
    [ProducesResponseType(typeof(ResponseDTO<StepDTO>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ResponseDTO<ICollection<StepDTO>>>> All()
    {
        var specification = new StepSpecification();
        var query = new ListStepAllWithSpecAsyncQuery(specification);
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);

    }
    
    [AuthorizeByRole(AppConstant.AdminRoleName)]
    [HttpPut]
    [ProducesResponseType(typeof(ResponseDTO<StepDTO>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ResponseDTO<StepDTO>>> Update([FromBody] StepUpdateDTO model,long Id)
    {
        var query = new UpdateStepCommand(Id,model);
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);

    }
    
    
}
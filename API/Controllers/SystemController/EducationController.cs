using System.Net;
using API.Helpers;
using Application.DTO.Common;
using Application.DTO.System;
using Application.Features.EducationCQRS.Command.AddEducation;
using Application.Features.EducationCQRS.Command.DeleteEducation;
using Application.Features.EducationCQRS.Command.UpdateEducation;
using Application.Features.EducationCQRS.Query.GetEducationByIdAsync;
using Application.Features.EducationCQRS.Query.GetEducationWithSpecAsync;
using Application.Features.EducationCQRS.Query.ListEducationAllAsync;
using Application.Features.EducationCQRS.Query.ListEducationWithSpecAsync;
using Infrastracture.Contracts.Parameters.EducationParameters;
using Infrastracture.Contracts.Specifications.EducationSpecification;
using Infrastracture.Helpers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.SystemController;

public class EducationController : BaseApiController
{
    private readonly IMediator _mediator;

    public EducationController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }
    
    [HttpGet]
    [ProducesResponseType(typeof(ResponseDTO<EducationDTO>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<EducationDTO>> GetById([FromQuery] long Id)
    {
        var query = new GetEducationByIdAsyncQuery(Id);
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);

    }
    [HttpGet]
    [ProducesResponseType(typeof(ResponseDTO<EducationDTO>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<EducationDTO>> Get([FromQuery] EducationParameter parameter)
    {
        var specification = new EducationSpecification(parameter);
        var query = new GetEducationWithSpecAsyncQuery(specification);
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);

    }
    [HttpGet]
    [ProducesResponseType(typeof(ResponseDTO<ICollection<EducationDTO>>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ResponseDTO<ICollection<EducationDTO>>>> GetAll()
    {
        var query = new ListEducationAllAsyncQuery();
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);

    }
    [HttpGet]
    [ProducesResponseType(typeof(ResponseDTO<ICollection<EducationDTO>>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ResponseDTO<ICollection<EducationDTO>>>> All([FromQuery] EducationParameter parameter)
    {
        var specification = new EducationSpecification(parameter);
        var query = new ListEducationWithSpecQuery(specification);
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);

    }
    
    [AuthorizeByRole(AppConstant.AdminRoleName)]
    [HttpPost]
    [ProducesResponseType(typeof(ResponseDTO<EducationDTO>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ResponseDTO<EducationDTO>>> Create([FromBody] EducationDTO model)
    {
        var query = new AddEducationCommand(model);
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);

    }
   
    [AuthorizeByRole(AppConstant.AdminRoleName)]
    [HttpPut]
    [ProducesResponseType(typeof(ResponseDTO<EducationDTO>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ResponseDTO<EducationDTO>>> Update([FromBody] EducationDTO model,long Id)
    {
        var query = new UpdateEducationCommand(model,Id);
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);

    }
    
    [AuthorizeByRole(AppConstant.AdminRoleName)]
    [HttpDelete]
    [ProducesResponseType(typeof(ResponseDTO<bool>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ResponseDTO<bool>>> Delete(long Id)
    {
        var query = new DeleteEducationCommand(Id);
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);

    }
}
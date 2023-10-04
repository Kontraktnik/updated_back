using System.Net;
using API.Helpers;
using Application.DTO.Common;
using Application.DTO.System;
using Application.Features.MedicalStatusCQRS.Command.AddMedicalStatus;
using Application.Features.MedicalStatusCQRS.Command.DeleteMedicalStatus;
using Application.Features.MedicalStatusCQRS.Command.UpdateMedicalStatus;
using Application.Features.MedicalStatusCQRS.Query.GetMedicalStatusByIdAsync;
using Application.Features.MedicalStatusCQRS.Query.GetMedicalStatusWithSpecAsync;
using Application.Features.MedicalStatusCQRS.Query.ListMedicalStatusAllAsync;
using Application.Features.MedicalStatusCQRS.Query.ListMedicalStatusWithSpecAsync;
using Infrastracture.Contracts.Parameters.MedicalStatusParameters;
using Infrastracture.Contracts.Specifications.MedicalStatusSpecification;
using Infrastracture.Helpers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.SystemController;

public class MedicalStatusController : BaseApiController
{
    private readonly IMediator _mediator;

    public MedicalStatusController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }
    
    [HttpGet]
    [ProducesResponseType(typeof(ResponseDTO<RelativeDTO>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<MedicalStatusDTO>> GetById([FromQuery] long Id)
    {
        var query = new GetMedicalStatusByIdAsyncQuery(Id);
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);

    }
    
    
    
    [HttpGet]
    [ProducesResponseType(typeof(ResponseDTO<MedicalStatusDTO>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<MedicalStatusDTO>> Get([FromQuery] MedicalStatusParameter parameter)
    {
        var specification = new MedicalStatusSpecification(parameter);
        var query = new GetMedicalStatusWithSpecAsyncQuery(specification);
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);

    }
    [HttpGet]
    [ProducesResponseType(typeof(ResponseDTO<ICollection<MedicalStatusDTO>>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ResponseDTO<ICollection<MedicalStatusDTO>>>> GetAll()
    {
        var query = new ListMedicalStatusAllAsyncQuery();
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);

    }
    [HttpGet]
    [ProducesResponseType(typeof(ResponseDTO<ICollection<MedicalStatusDTO>>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ResponseDTO<ICollection<MedicalStatusDTO>>>> All([FromQuery] MedicalStatusParameter parameter)
    {
        var specification = new MedicalStatusSpecification(parameter);
        var query = new ListMedicalStatusWithSpecAsyncQuery(specification);
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);

    }
    
    [AuthorizeByRole(AppConstant.AdminRoleName)]
    [HttpPost]
    [ProducesResponseType(typeof(ResponseDTO<MedicalStatusDTO>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ResponseDTO<MedicalStatusDTO>>> Create([FromBody] MedicalStatusDTO model)
    {
        var query = new AddMedicalStatusCommand(model);
        var area =  await _mediator.Send(query);
        return Ok(area);
    }
   
    [AuthorizeByRole(AppConstant.AdminRoleName)]
    [HttpPut]
    [ProducesResponseType(typeof(ResponseDTO<MedicalStatusDTO>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ResponseDTO<MedicalStatusDTO>>> Update([FromBody] MedicalStatusDTO model,long Id)
    {
        var query = new UpdateMedicalStatusCommand(model,Id);
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);

    }
    
    [AuthorizeByRole(AppConstant.AdminRoleName)]
    [HttpDelete]
    [ProducesResponseType(typeof(ResponseDTO<bool>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ResponseDTO<bool>>> Delete(long Id)
    {
        var query = new DeleteMedicalStatusCommand(Id);
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);

    }
}
using System.Net;
using API.Helpers;
using Application.DTO.Calculation;
using Application.DTO.Common;
using Application.Features.QualificationCQRS.Command.AddQualification;
using Application.Features.QualificationCQRS.Command.DeleteQualification;
using Application.Features.QualificationCQRS.Command.UpdateQualification;
using Application.Features.QualificationCQRS.Query.GetQualificationByIdAsync;
using Application.Features.QualificationCQRS.Query.GetQualificationWithSpecAsync;
using Application.Features.QualificationCQRS.Query.ListQualificationAllAsync;
using Application.Features.QualificationCQRS.Query.ListQualificationWithSpecAsync;
using Infrastracture.Contracts.Parameters.QualificationParameters;
using Infrastracture.Contracts.Specifications.QualificationSpecification;
using Infrastracture.Helpers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.CalculationController;

public class QualificationController : BaseApiController
{
    private readonly IMediator _mediator;

    public QualificationController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }
    
    [HttpGet]
    [ProducesResponseType(typeof(ResponseDTO<QualificationDTO>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<QualificationDTO>> GetById([FromQuery] long Id)
    {
        var query = new GetQualificationByIdAsyncQuery(Id);
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);

    }
    [HttpGet]
    [ProducesResponseType(typeof(ResponseDTO<QualificationDTO>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<QualificationDTO>> Get([FromQuery] QualificationParameter parameter)
    {
        var specification = new QualificationSpecification(parameter);
        var query = new GetQualificationWithSpecAsyncQuery(specification);
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);

    }
    [HttpGet]
    [ProducesResponseType(typeof(ResponseDTO<ICollection<QualificationDTO>>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ResponseDTO<ICollection<QualificationDTO>>>> GetAll()
    {
        var query = new ListQualificationAllAsyncQuery();
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);

    }
    [HttpGet]
    [ProducesResponseType(typeof(ResponseDTO<ICollection<QualificationDTO>>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ResponseDTO<ICollection<QualificationDTO>>>> All([FromQuery] QualificationParameter parameter)
    {
        var specification = new QualificationSpecification(parameter);
        var query = new ListQualificationWithSpecAsyncQuery(specification);
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);

    }
    
    [AuthorizeByRole(AppConstant.AdminRoleName)]
    [HttpPost]
    [ProducesResponseType(typeof(ResponseDTO<QualificationDTO>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ResponseDTO<QualificationDTO>>> Create([FromBody] QualificationDTO model)
    {
        var query = new AddQualificationCommand(model);
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);

    }
   
    [AuthorizeByRole(AppConstant.AdminRoleName)]
    [HttpPut]
    [ProducesResponseType(typeof(ResponseDTO<QualificationDTO>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ResponseDTO<QualificationDTO>>> Update([FromBody] QualificationDTO model,long Id)
    {
        var query = new UpdateQualificationCommand(model,Id);
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);

    }
    
    [AuthorizeByRole(AppConstant.AdminRoleName)]
    [HttpDelete]
    [ProducesResponseType(typeof(ResponseDTO<bool>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ResponseDTO<bool>>> Delete(long Id)
    {
        var query = new DeleteQualificationCommand(Id);
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);

    }
}
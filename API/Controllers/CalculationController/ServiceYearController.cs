using System.Net;
using API.Helpers;
using Application.DTO.Calculation;
using Application.DTO.Common;
using Application.Features.ServiceYearCQRS.Command.AddServiceYear;
using Application.Features.ServiceYearCQRS.Command.DeleteServiceYear;
using Application.Features.ServiceYearCQRS.Command.UpdateServiceYear;
using Application.Features.ServiceYearCQRS.Query.GetServiceYearByIdAsync;
using Application.Features.ServiceYearCQRS.Query.GetServiceYearWithSpecAsync;
using Application.Features.ServiceYearCQRS.Query.ListServiceYearAllAsync;
using Application.Features.ServiceYearCQRS.Query.ListServiceYearWithSpecAsync;
using Infrastracture.Contracts.Parameters.ServiceYearParameters;
using Infrastracture.Contracts.Specifications.ServiceYearSpecification;
using Infrastracture.Helpers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.CalculationController;

public class ServiceYearController : BaseApiController
{
    private readonly IMediator _mediator;

    public ServiceYearController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }
    
    [HttpGet]
    [ProducesResponseType(typeof(ResponseDTO<ServiceYearDTO>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ServiceYearDTO>> GetById([FromQuery] long Id)
    {
        var query = new GetServiceYearByIdAsyncQuery(Id);
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);
    }
    [HttpGet]
    [ProducesResponseType(typeof(ResponseDTO<ServiceYearDTO>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ServiceYearDTO>> Get([FromQuery] ServiceYearParameter parameter)
    {
        var specification = new ServiceYearSpecification(parameter);
        var query = new GetServiceYearWithSpecAsyncQuery(specification);
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);
    }
    [HttpGet]
    [ProducesResponseType(typeof(ResponseDTO<ICollection<ServiceYearDTO>>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ResponseDTO<ICollection<ServiceYearDTO>>>> GetAll()
    {
        var query = new ListServiceYearAllAsyncQuery();
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);
    }
    [HttpGet]
    [ProducesResponseType(typeof(ResponseDTO<ICollection<ServiceYearDTO>>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ResponseDTO<ICollection<ServiceYearDTO>>>> All([FromQuery] ServiceYearParameter parameter)
    {
        var specification = new ServiceYearSpecification(parameter);
        var query = new ListServiceYearWithSpecAsyncQuery(specification);
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);

    }
    
    [AuthorizeByRole(AppConstant.AdminRoleName)]
    [HttpPost]
    [ProducesResponseType(typeof(ResponseDTO<ServiceYearDTO>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ResponseDTO<ServiceYearDTO>>> Create([FromBody] ServiceYearDTO model)
    {
        var query = new AddServiceYearCommand(model);
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);

    }
   
    [AuthorizeByRole(AppConstant.AdminRoleName)]
    [HttpPut]
    [ProducesResponseType(typeof(ResponseDTO<ServiceYearDTO>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ResponseDTO<ServiceYearDTO>>> Update([FromBody] ServiceYearDTO model,long Id)
    {
        var query = new UpdateServiceYearCommand(model,Id);
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);

    }
    
    [AuthorizeByRole(AppConstant.AdminRoleName)]
    [HttpDelete]
    [ProducesResponseType(typeof(ResponseDTO<bool>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ResponseDTO<bool>>> Delete(long Id)
    {
        var query = new DeleteServiceYearCommand(Id);
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);

    }
}
using System.Net;
using API.Helpers;
using Application.DTO.Common;
using Application.DTO.System;
using Application.Features.DriverLicenseCQRS.Command.AddDriverLicense;
using Application.Features.DriverLicenseCQRS.Command.DeleteDriverLicense;
using Application.Features.DriverLicenseCQRS.Command.UpdateDriverLicense;
using Application.Features.DriverLicenseCQRS.Query.GetDriverLicenseByIdAsync;
using Application.Features.DriverLicenseCQRS.Query.GetDriverLicenseWithSpecAsync;
using Application.Features.DriverLicenseCQRS.Query.ListDriverLicenseAllAsync;
using Application.Features.DriverLicenseCQRS.Query.ListDriverLicenseWithSpecAsync;
using Infrastracture.Contracts.Parameters.DriverLicenseParameters;
using Infrastracture.Contracts.Specifications.DriverLicenseSpecification;
using Infrastracture.Helpers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.SystemController;

public class DriverLicenseController : BaseApiController
{
    private readonly IMediator _mediator;

    public DriverLicenseController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }
    
    [HttpGet]
    [ProducesResponseType(typeof(ResponseDTO<DriverLicenseDTO>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<DriverLicenseDTO>> GetById([FromQuery] long Id)
    {
        var query = new GetDriverLicenseByIdAsyncQuery(Id);
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);

    }
    [HttpGet]
    [ProducesResponseType(typeof(ResponseDTO<DriverLicenseDTO>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<DriverLicenseDTO>> Get([FromQuery] DriverLicenseParameter parameter)
    {
        var specification = new DriverLicenseSpecification(parameter);
        var query = new GetDriverLicenseWithSpecAsyncQuery(specification);
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);

    }
    [HttpGet]
    [ProducesResponseType(typeof(ResponseDTO<ICollection<DriverLicenseDTO>>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ResponseDTO<ICollection<DriverLicenseDTO>>>> GetAll()
    {
        var query = new ListDriverLicenseAllAsyncQuery();
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);

    }
    [HttpGet]
    [ProducesResponseType(typeof(ResponseDTO<ICollection<DriverLicenseDTO>>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ResponseDTO<ICollection<DriverLicenseDTO>>>> All([FromQuery] DriverLicenseParameter parameter)
    {
        var specification = new DriverLicenseSpecification(parameter);
        var query = new ListDriverLicenseWithSpecAsyncQuery(specification);
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);

    }
    
    [AuthorizeByRole(AppConstant.AdminRoleName)]
    [HttpPost]
    [ProducesResponseType(typeof(ResponseDTO<DriverLicenseDTO>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ResponseDTO<DriverLicenseDTO>>> Create([FromBody] DriverLicenseDTO model)
    {
        var query = new AddDriverLicenseCommand(model);
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);

    }
   
    [AuthorizeByRole(AppConstant.AdminRoleName)]
    [HttpPut]
    [ProducesResponseType(typeof(ResponseDTO<DriverLicenseDTO>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ResponseDTO<DriverLicenseDTO>>> Update([FromBody] DriverLicenseDTO model,long Id)
    {
        var query = new UpdateDriverLicenseCommand(model,Id);
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);

    }
    
    [AuthorizeByRole(AppConstant.AdminRoleName)]
    [HttpDelete]
    [ProducesResponseType(typeof(ResponseDTO<bool>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ResponseDTO<bool>>> Delete(long Id)
    {
        var query = new DeleteDriverLicenseCommand(Id);
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);

    }
}
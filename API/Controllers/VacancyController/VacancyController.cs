using System.Net;
using System.Security.Claims;
using API.Helpers;
using API.Model;
using Application.DTO.Common;
using Application.DTO.User;
using Application.DTO.Vacancy;
using Application.Features.UserCQRS.Query.GetUserByIdAsync;
using Application.Features.VacancyCQRS.Command.AddVacancy;
using Application.Features.VacancyCQRS.Command.DeleteVacancy;
using Application.Features.VacancyCQRS.Command.UpdateVacancy;
using Application.Features.VacancyCQRS.Query.CountVacancyAsync;
using Application.Features.VacancyCQRS.Query.GetVacancyByIdAsync;
using Application.Features.VacancyCQRS.Query.ListVacancyWithSpecAsync;
using Infrastracture.Contracts.Parameters.VacancyParameters;
using Infrastracture.Contracts.Specifications.UserSpecification;
using Infrastracture.Contracts.Specifications.VacancySpecification;
using Infrastracture.Helpers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.VacancyController;

public class VacancyController : BaseApiController
{
    private readonly IMediator _mediator;

    public VacancyController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }
    
    
    
    [HttpGet]
    [ProducesResponseType(typeof(ResponseDTO<VacancyRDTO>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ResponseDTO<VacancyRDTO>>> Get([FromQuery] long Id)
    {
        var specification = new VacancySpecification(Id, true,null);
        var query = new GetVacancyByIdAsyncQuery(specification);
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);

    }
    
    [HttpGet]
    [ProducesResponseType(typeof(Pagination<VacancyRDTO>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<Pagination<VacancyRDTO>>> All([FromQuery] VacancyParameter parameter)
    {
        var specification = new VacancySpecification(parameter, true);
        var query = new ListVacancyWithSpecAsyncQuery(specification);
        var models =  await _mediator.Send(query);
        var pagination = new Pagination<VacancyRDTO>(
            parameter.PageIndex,
            parameter.PageSize,
            (await _mediator.Send(new CountVacancyAsyncQuery(new CountVacancySpecification(parameter,true)))),
            models.Data
        );
        return Ok(pagination);
    }

    [AuthorizeByRole(AppConstant.DirectorName, AppConstant.ExecutorName)]
    [HttpPost]
    [ProducesResponseType(typeof(ResponseDTO<VacancyRDTO>), (int)HttpStatusCode.Created)]
    public async Task<ActionResult<ResponseDTO<VacancyRDTO>>> Create([FromBody] VacancyCUDTO model)
    {
        var result = await _mediator.Send(new AddVacancyCommand(model,
            new UserSpecification(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, null)));
        return StatusCode(result.StatusCode,result);
    }
    
    [AuthorizeByRole(AppConstant.DirectorName, AppConstant.ExecutorName)]
    [HttpPut]
    [ProducesResponseType(typeof(ResponseDTO<VacancyRDTO>), (int)HttpStatusCode.Created)]
    public async Task<ActionResult<ResponseDTO<VacancyRDTO>>> Update([FromBody] VacancyCUDTO model,long Id)
    {
        var result = await _mediator.Send(new UpdateVacancyCommand(model,
            new UserSpecification(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, null),Id));
        return StatusCode(result.StatusCode,result);

    }
    
    [AuthorizeByRole(AppConstant.DirectorName, AppConstant.ExecutorName)]
    [HttpGet]
    [ProducesResponseType(typeof(ResponseDTO<VacancyRDTO>), (int)HttpStatusCode.Created)]
    public async Task<ActionResult<ResponseDTO<VacancyRDTO>>> GetForUpdate([FromQuery] long Id)
    {
        var userData =  await  _mediator.Send(new GetUserByIdAsync(new UserSpecification(User.FindFirst(ClaimTypes.NameIdentifier)?.Value,null)));
        var specification = new VacancyExecutorSpecification(Id,userData.Data.AreaId??0,null);
        var query = new GetVacancyByIdAsyncQuery(specification);
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);

    }
    
    [AuthorizeByRole(AppConstant.DirectorName, AppConstant.ExecutorName)]
    [HttpGet]
    [ProducesResponseType(typeof(Pagination<VacancyRDTO>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<Pagination<VacancyRDTO>>> MyAll([FromQuery] VacancyExecutorParameter parameter)
    {
        var userData =  await  _mediator.Send(new GetUserByIdAsync(new UserSpecification(User.FindFirst(ClaimTypes.NameIdentifier)?.Value,null)));
        var specification = new VacancyExecutorSpecification(parameter, userData.Data.AreaId??0);
        var query = new ListVacancyWithSpecAsyncQuery(specification);
        var models =  await _mediator.Send(query);
        var pagination = new Pagination<VacancyRDTO>(
            parameter.PageIndex,
            parameter.PageSize,
            (await _mediator.Send(new CountVacancyAsyncQuery(new VacancyExecutorSpecification(parameter, userData.Data.AreaId??0,false)))),
            models.Data
        );
        return Ok(pagination);
    }
    
    [AuthorizeByRole(AppConstant.DirectorName, AppConstant.ExecutorName)]
    [HttpDelete]
    [ProducesResponseType(typeof(ResponseDTO<bool>), (int)HttpStatusCode.Created)]
    public async Task<ActionResult<ResponseDTO<bool>>> Delete([FromQuery] long Id)
    {
        var userData =  await  _mediator.Send(new GetUserByIdAsync(new UserSpecification(User.FindFirst(ClaimTypes.NameIdentifier)?.Value,null)));
        var result = await _mediator.Send(new DeleteVacancyCommand(Id,userData.Data));
        return StatusCode(result.StatusCode,result);

    }
    
}
using System.Net;
using API.Helpers;
using API.Model;
using Application.DTO.Calculation.JobYearDTO;
using Application.DTO.Common;
using Application.Features.JobCategoryCQRS.Query.GetJobCategoryByIdAsync;
using Application.Features.JobYearCQRS.Command.AddJobYear;
using Application.Features.JobYearCQRS.Command.DeleteJobYear;
using Application.Features.JobYearCQRS.Command.UpdateJobYear;
using Application.Features.JobYearCQRS.Query.CountJobYearAsync;
using Application.Features.JobYearCQRS.Query.GetJobYearByIdAsync;
using Application.Features.JobYearCQRS.Query.ListJobYearAllAsync;
using Application.Features.JobYearCQRS.Query.ListJobYearWithSpecAsync;
using Infrastracture.Contracts.Parameters.JobYearParameters;
using Infrastracture.Contracts.Specifications.JobYearSpecification;
using Infrastracture.Helpers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.CalculationController;

public class JobYearController : BaseApiController
{
    private readonly IMediator _mediator;

    public JobYearController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }
    
    [HttpGet]
    [ProducesResponseType(typeof(ResponseDTO<JobYearRDTO>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<JobYearRDTO>> GetById([FromQuery] long Id)
    {
        var specification = new JobYearSpecification(Id);
        var query = new GetJobYearByIdAsyncQuery(specification);
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);
        
    }
    [HttpGet]
    [ProducesResponseType(typeof(ResponseDTO<JobYearRDTO>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<JobYearRDTO>> Get([FromQuery] long Id)
    {
        var specification = new JobYearSpecification(Id);
        var query = new GetJobYearByIdAsyncQuery(specification);
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);
    }
    [HttpGet]
    [ProducesResponseType(typeof(ResponseDTO<ICollection<JobYearRDTO>>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ResponseDTO<ICollection<JobYearRDTO>>>> GetAll()
    {
        var query = new ListJobYearAllAsyncQuery();
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);
    }
    [HttpGet]
    [ProducesResponseType(typeof(Pagination<JobYearRDTO>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<Pagination<JobYearRDTO>>> All([FromQuery] JobYearParameter parameter)
    {
        var specification = new JobYearSpecification(parameter);
        var query = new ListJobYearWithSpecAsyncQuery(specification);
        var result =  await _mediator.Send(query);
        var pagination = new Pagination<JobYearRDTO>(
                parameter.PageIndex,
                parameter.PageSize,
                (await _mediator.Send(new CountJobYearAsyncQuery(new JobYearSpecification(parameter,false) ))),
                result.Data
                
        );
        return StatusCode(result.StatusCode,pagination);
    }
    
    [AuthorizeByRole(AppConstant.AdminRoleName)]
    [HttpPost]
    [ProducesResponseType(typeof(ResponseDTO<JobYearRDTO>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ResponseDTO<JobYearRDTO>>> Create([FromBody] JobYearCUDTO model)
    {
        var query = new AddJobYearCommand(model);
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);
    }
   
    [AuthorizeByRole(AppConstant.AdminRoleName)]
    [HttpPut]
    [ProducesResponseType(typeof(ResponseDTO<JobYearRDTO>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ResponseDTO<JobYearRDTO>>> Update([FromBody] JobYearCUDTO model,long Id)
    {
        var query = new UpdateJobYearCommand(model,Id);
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);
    }
    
    [AuthorizeByRole(AppConstant.AdminRoleName)]
    [HttpDelete]
    [ProducesResponseType(typeof(ResponseDTO<bool>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ResponseDTO<bool>>> Delete(long Id)
    {
        var query = new DeleteJobYearCommand(Id);
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);
    }
}
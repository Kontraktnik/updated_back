using System.Net;
using API.Helpers;
using Application.DTO.Calculation;
using Application.DTO.Common;
using Application.Features.JobCategoryCQRS.Command.AddJobCategory;
using Application.Features.JobCategoryCQRS.Command.DeleteJobCategory;
using Application.Features.JobCategoryCQRS.Command.UpdateJobCategory;
using Application.Features.JobCategoryCQRS.Query.GetJobCategoryByIdAsync;
using Application.Features.JobCategoryCQRS.Query.GetJobCategoryWithSpecAsync;
using Application.Features.JobCategoryCQRS.Query.ListJobCategoryAllAsync;
using Application.Features.JobCategoryCQRS.Query.ListJobCategoryWithSpecAsync;
using Infrastracture.Contracts.Parameters.JobCategoryParameters;
using Infrastracture.Contracts.Specifications.JobCategorySpecification;
using Infrastracture.Helpers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.CalculationController;
 
public class JobCategoryController : BaseApiController
{
     private readonly IMediator _mediator;

    public JobCategoryController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }
    
    [HttpGet]
    [ProducesResponseType(typeof(ResponseDTO<JobCategoryDTO>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<JobCategoryDTO>> GetById([FromQuery] long Id)
    {
        var query = new GetJobCategoryByIdAsyncQuery(Id);
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);
    }
    [HttpGet]
    [ProducesResponseType(typeof(ResponseDTO<JobCategoryDTO>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<JobCategoryDTO>> Get([FromQuery] JobCategoryParameter parameter)
    {
        var specification = new JobCategorySpecification(parameter);
        var query = new GetJobCategoryWithSpecAsyncQuery(specification);
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);
    }
    [HttpGet]
    [ProducesResponseType(typeof(ResponseDTO<ICollection<JobCategoryDTO>>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ResponseDTO<ICollection<JobCategoryDTO>>>> GetAll()
    {
        var query = new ListJobCategoryAllAsyncQuery();
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);
    }
    [HttpGet]
    [ProducesResponseType(typeof(ResponseDTO<ICollection<JobCategoryDTO>>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ResponseDTO<ICollection<JobCategoryDTO>>>> All([FromQuery] JobCategoryParameter parameter)
    {
        var specification = new JobCategorySpecification(parameter);
        var query = new ListJobCategoryWithSpecAsyncQuery(specification);
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);
    }
    
    [AuthorizeByRole(AppConstant.AdminRoleName)]
    [HttpPost]
    [ProducesResponseType(typeof(ResponseDTO<JobCategoryDTO>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ResponseDTO<JobCategoryDTO>>> Create([FromBody] JobCategoryDTO model)
    {
        var query = new AddJobCategoryCommand(model);
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);
    }
   
    [AuthorizeByRole(AppConstant.AdminRoleName)]
    [HttpPut]
    [ProducesResponseType(typeof(ResponseDTO<JobCategoryDTO>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ResponseDTO<JobCategoryDTO>>> Update([FromBody] JobCategoryDTO model,long Id)
    {
        var query = new UpdateJobCategoryCommand(model,Id);
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);
    }
    
    [AuthorizeByRole(AppConstant.AdminRoleName)]
    [HttpDelete]
    [ProducesResponseType(typeof(ResponseDTO<bool>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ResponseDTO<bool>>> Delete(long Id)
    {
        var query = new DeleteJobCategoryCommand(Id);
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);
    }
}
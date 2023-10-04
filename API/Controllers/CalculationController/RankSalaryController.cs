using System.Net;
using API.Helpers;
using API.Model;
using Application.DTO.Calculation.RankSalaryDTO;
using Application.DTO.Common;
using Application.Features.ArmyDepartmentCQRS.Query.GetArmyDepartmentByIdAsync;
using Application.Features.RankSalaryCQRS.Command.AddRankSalary;
using Application.Features.RankSalaryCQRS.Command.DeleteRankSalary;
using Application.Features.RankSalaryCQRS.Command.UpdateRankSalary;
using Application.Features.RankSalaryCQRS.Query.CountRankSalaryAsync;
using Application.Features.RankSalaryCQRS.Query.GetRankSalaryByIdAsync;
using Application.Features.RankSalaryCQRS.Query.GetRankSalaryWithSpecAsync;
using Application.Features.RankSalaryCQRS.Query.ListRankSalaryAllAsync;
using Application.Features.RankSalaryCQRS.Query.ListRankSalaryWithSpecAsync;
using Infrastracture.Contracts.Parameters.RankSalaryParameters;
using Infrastracture.Contracts.Specifications.RankSalarySpecification;
using Infrastracture.Helpers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.CalculationController;

public class RankSalaryController : BaseApiController
{
    private readonly IMediator _mediator;

    public RankSalaryController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }
    
    [HttpGet]
    [ProducesResponseType(typeof(ResponseDTO<RankSalaryRDTO>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<RankSalaryRDTO>> GetById([FromQuery] long Id)
    {
        var specification = new RankSalarySpecification(Id, null);
        var query = new GetRankSalaryByIdAsyncQuery(specification);
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);

    }
    
    [HttpGet]
    [ProducesResponseType(typeof(ResponseDTO<RankSalaryRDTO>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<RankSalaryRDTO>> Get([FromQuery] RankSalaryParameter parameter)
    {
        var specification = new RankSalarySpecification(parameter);
        var query = new GetRankSalaryWithSpecAsyncQuery(specification);
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);

    }
    
    [HttpGet]
    [ProducesResponseType(typeof(ResponseDTO<ICollection<RankSalaryRDTO>>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ResponseDTO<ICollection<RankSalaryRDTO>>>> GetAll()
    {
        var query = new ListRankSalaryAllAsyncQuery();
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);

    }
    [HttpGet]
    [ProducesResponseType(typeof(Pagination<RankSalaryRDTO>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<Pagination<RankSalaryRDTO>>> All([FromQuery] RankSalaryParameter parameter)
    {
        var specification = new RankSalarySpecification(parameter);
        var query = new ListRankSalaryWithSpecAsyncQuery(specification);
        var result =  await _mediator.Send(query);
        var pagination = new Pagination<RankSalaryRDTO>(
            parameter.PageIndex,
            parameter.PageSize,
            (await _mediator.Send(new CountRankSalaryAsyncQuery( new RankSalarySpecification(parameter,false)))),
            result.Data
        );
        return StatusCode(result.StatusCode,pagination);

    }
    
    [AuthorizeByRole(AppConstant.AdminRoleName)]
    [HttpPost]
    [ProducesResponseType(typeof(ResponseDTO<RankSalaryRDTO>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ResponseDTO<RankSalaryRDTO>>> Create([FromBody] RankSalaryCUDTO model)
    {
        var query = new AddRankSalaryCommand(model);
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);

    }
   
    [AuthorizeByRole(AppConstant.AdminRoleName)]
    [HttpPut]
    [ProducesResponseType(typeof(ResponseDTO<RankSalaryRDTO>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ResponseDTO<RankSalaryRDTO>>> Update([FromBody] RankSalaryCUDTO model,long Id)
    {
        var query = new UpdateRankSalaryCommand(model,Id);
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);

    }
    
    [AuthorizeByRole(AppConstant.AdminRoleName)]
    [HttpDelete]
    [ProducesResponseType(typeof(ResponseDTO<bool>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ResponseDTO<bool>>> Delete(long Id)
    {
        var query = new DeleteRankSalaryCommand(Id);
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);

    }
}
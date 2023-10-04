using System.Net;
using API.Helpers;
using Application.DTO.Calculation;
using Application.DTO.Common;
using Application.DTO.System;
using Application.Features.CategoryPositionCQRS.Command.AddCategoryPosition;
using Application.Features.CategoryPositionCQRS.Command.DeleteCategoryPosition;
using Application.Features.CategoryPositionCQRS.Command.UpdateCategoryPosition;
using Application.Features.CategoryPositionCQRS.Query.GetCategoryPositionByIdAsync;
using Application.Features.CategoryPositionCQRS.Query.GetCategoryPositionWithSpecAsync;
using Application.Features.CategoryPositionCQRS.Query.ListCategoryPositionAllAsync;
using Application.Features.CategoryPositionCQRS.Query.ListCategoryPositionWithSpecAsync;
using Infrastracture.Contracts.Parameters.CategoryPositionParameters;
using Infrastracture.Contracts.Specifications.CategoryPositionSpecification;
using Infrastracture.Helpers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.CalculationController;

public class CategoryPositionController : BaseApiController
{
    private readonly IMediator _mediator;

    public CategoryPositionController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }
    
    [HttpGet]
    [ProducesResponseType(typeof(ResponseDTO<DriverLicenseDTO>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<CategoryPositionDTO>> GetById([FromQuery] long Id)
    {
        var query = new GetCategoryPositionByIdAsyncQuery(Id);
        var area =  await _mediator.Send(query);
        return StatusCode(area.StatusCode, area);
    }
    [HttpGet]
    [ProducesResponseType(typeof(ResponseDTO<CategoryPositionDTO>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<CategoryPositionDTO>> Get([FromQuery] CategoryPositionParameter parameter)
    {
        var specification = new CategoryPositionSpecification(parameter);
        var query = new GetCategoryPositionWithSpecAsyncQuery(specification);
        var area =  await _mediator.Send(query);
        return StatusCode(area.StatusCode, area);
    }
    [HttpGet]
    [ProducesResponseType(typeof(ResponseDTO<ICollection<CategoryPositionDTO>>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ResponseDTO<ICollection<CategoryPositionDTO>>>> GetAll()
    {
        var query = new ListCategoryPositionAllAsyncQuery();
        var area =  await _mediator.Send(query);
        return StatusCode(area.StatusCode, area);
    }
    [HttpGet]
    [ProducesResponseType(typeof(ResponseDTO<ICollection<CategoryPositionDTO>>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ResponseDTO<ICollection<CategoryPositionDTO>>>> All([FromQuery] CategoryPositionParameter parameter)
    {
        var specification = new CategoryPositionSpecification(parameter);
        var query = new ListCategoryPositionWithSpecAsyncQuery(specification);
        var area =  await _mediator.Send(query);
        return StatusCode(area.StatusCode, area);
    }
    
    [AuthorizeByRole(AppConstant.AdminRoleName)]
    [HttpPost]
    [ProducesResponseType(typeof(ResponseDTO<CategoryPositionDTO>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ResponseDTO<CategoryPositionDTO>>> Create([FromBody] CategoryPositionDTO model)
    {
        var query = new AddCategoryPositionCommand(model);
        var area =  await _mediator.Send(query);
        return StatusCode(area.StatusCode, area);
    }
   
    [AuthorizeByRole(AppConstant.AdminRoleName)]
    [HttpPut]
    [ProducesResponseType(typeof(ResponseDTO<CategoryPositionDTO>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ResponseDTO<CategoryPositionDTO>>> Update([FromBody] CategoryPositionDTO model,long Id)
    {
        var query = new UpdateCategoryPositionCommand(model,Id);
        var area =  await _mediator.Send(query);
        return StatusCode(area.StatusCode, area);
    }
    
    [AuthorizeByRole(AppConstant.AdminRoleName)]
    [HttpDelete]
    [ProducesResponseType(typeof(ResponseDTO<bool>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ResponseDTO<bool>>> Delete(long Id)
    {
        var query = new DeleteCategoryPositionCommand(Id);
        var area =  await _mediator.Send(query);
        return StatusCode(area.StatusCode, area);
    }
}
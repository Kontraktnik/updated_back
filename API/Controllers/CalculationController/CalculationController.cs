using Application.DTO.Calculation;
using Application.DTO.Calculation.PositionDTO;
using Application.DTO.Common;
using Application.Features.CalculationCQRS.Query.CalculateSalaryAsync;
using Application.Features.CalculationCQRS.Query.GetAreaSalariesAsync;
using Application.Features.PositionCQRS.Query.GetPositionByIdAsync;
using Domain.Models.CalculationModels;
using Infrastracture.Contracts.Specifications.PositionSpecification;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers.CalculationController;

public class CalculationController : BaseApiController
{
    private readonly IMediator _mediator;

    public CalculationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<ResponseDTO<int>>> CalculateSalary([FromBody] CalculationDTO model)
    {
        var query = new CalculateSalaryAsyncQuery(model);
        var result =await _mediator.Send(query);
        return StatusCode(result.StatusCode, result);
    }

    [HttpGet]
    [ProducesResponseType(typeof(ResponseDTO<AreaSalary[]>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<AreaSalary[]>> GetAreaSalaries()
    {
        var query = new GetAreaSalariesAsyncQuery();
        var result = await _mediator.Send(query);
        return StatusCode(result.StatusCode, result);

    }
}
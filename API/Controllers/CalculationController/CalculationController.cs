using Application.DTO.Calculation;
using Application.DTO.Common;
using Application.Features.CalculationCQRS.Query.CalculateSalaryAsync;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
}
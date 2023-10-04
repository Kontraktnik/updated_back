using System.Net;
using API.Helpers;
using Application.DTO.Calculation;
using Application.DTO.Common;
using Application.Features.SecretLevelCQRS.Command.AddSecretLevel;
using Application.Features.SecretLevelCQRS.Command.DeleteSecretLevel;
using Application.Features.SecretLevelCQRS.Command.UpdateSecretLevel;
using Application.Features.SecretLevelCQRS.Query.GetSecretLevelByIdAsync;
using Application.Features.SecretLevelCQRS.Query.GetSecretLevelWithSpecAsync;
using Application.Features.SecretLevelCQRS.Query.ListSecretLevelAllAsync;
using Application.Features.SecretLevelCQRS.Query.ListSecretLevelWithSpecAsync;
using Infrastracture.Contracts.Parameters.SecretLevelParameters;
using Infrastracture.Contracts.Specifications.SecretLevelSpecification;
using Infrastracture.Helpers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.CalculationController;

public class SecretLevelController : BaseApiController
{
    private readonly IMediator _mediator;

    public SecretLevelController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }
    
    [HttpGet]
    [ProducesResponseType(typeof(ResponseDTO<SecretLevelDTO>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<SecretLevelDTO>> GetById([FromQuery] long Id)
    {
        var query = new GetSecretLevelByIdAsyncQuery(Id);
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);

    }
    [HttpGet]
    [ProducesResponseType(typeof(ResponseDTO<SecretLevelDTO>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<SecretLevelDTO>> Get([FromQuery] SecretLevelParameter parameter)
    {
        var specification = new SecretLevelSpecification(parameter);
        var query = new GetSecretLevelWithSpecAsyncQuery(specification);
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);

    }
    [HttpGet]
    [ProducesResponseType(typeof(ResponseDTO<ICollection<SecretLevelDTO>>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ResponseDTO<ICollection<SecretLevelDTO>>>> GetAll()
    {
        var query = new ListSecretLevelAllAsyncQuery();
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);

    }
    [HttpGet]
    [ProducesResponseType(typeof(ResponseDTO<ICollection<SecretLevelDTO>>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ResponseDTO<ICollection<SecretLevelDTO>>>> All([FromQuery] SecretLevelParameter parameter)
    {
        var specification = new SecretLevelSpecification(parameter);
        var query = new ListSecretLevelWithSpecAsyncQuery(specification);
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);

    }
    
    [AuthorizeByRole(AppConstant.AdminRoleName)]
    [HttpPost]
    [ProducesResponseType(typeof(ResponseDTO<SecretLevelDTO>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ResponseDTO<SecretLevelDTO>>> Create([FromBody] SecretLevelDTO model)
    {
        var query = new AddSecretLevelCommand(model);
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);

    }
   
    [AuthorizeByRole(AppConstant.AdminRoleName)]
    [HttpPut]
    [ProducesResponseType(typeof(ResponseDTO<SecretLevelDTO>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ResponseDTO<SecretLevelDTO>>> Update([FromBody] SecretLevelDTO model,long Id)
    {
        var query = new UpdateSecretLevelCommand(model,Id);
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);

    }
    
    [AuthorizeByRole(AppConstant.AdminRoleName)]
    [HttpDelete]
    [ProducesResponseType(typeof(ResponseDTO<bool>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ResponseDTO<bool>>> Delete(long Id)
    {
        var query = new DeleteSecretLevelCommand(Id);
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);

    }
}
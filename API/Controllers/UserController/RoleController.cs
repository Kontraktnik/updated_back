using System.Net;
using Application.DTO.Common;
using Application.DTO.System;
using Application.DTO.User;
using Application.Features.AreaCQRS.Query.GetAreaByIdAsync;
using Application.Features.RoleCQRS.Query.GetRoleByIdAsync;
using Application.Features.RoleCQRS.Query.ListRoleAllAsync;
using Domain.Models.UserModels;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.UserController;

public class RoleController : BaseApiController
{
    private readonly IMediator _mediator;

    public RoleController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }
    
    [Authorize]
    [HttpGet]
    [ProducesResponseType(typeof(ResponseDTO<RoleDTO>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ResponseDTO<RoleDTO>>> GetById(long Id)
    {
        var query = new GetRoleByIdAsyncQuery(Id);
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);

    }
    
    [Authorize]
    [HttpGet]
    [ProducesResponseType(typeof(ResponseDTO<ICollection<RoleDTO>>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ResponseDTO<ICollection<RoleDTO>>>> GetAll()
    {
        var query = new ListRoleAllAsyncQuery();
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);

    }
    
}
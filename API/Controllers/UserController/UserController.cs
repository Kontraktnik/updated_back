using System.Net;
using API.Helpers;
using API.Model;
using Application.DTO.Common;
using Application.DTO.User;
using Application.Features.UserCQRS.Command.AddUser;
using Application.Features.UserCQRS.Command.UpdateUser;
using Application.Features.UserCQRS.Query.CountUserAsync;
using Application.Features.UserCQRS.Query.GetUserByIdAsync;
using Application.Features.UserCQRS.Query.GetUserWithSpecAsync;
using Application.Features.UserCQRS.Query.ListUserWithSpecAsync;
using Infrastracture.Contracts.Parameters.UserParameters;
using Infrastracture.Contracts.Specifications.UserSpecification;
using Infrastracture.Helpers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.UserController;

public class UserController : BaseApiController
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }
    [AuthorizeByRole(AppConstant.AdminRoleName)]

    [HttpGet]
    [ProducesResponseType(typeof(ResponseDTO<UserDTO>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<UserDTO>> GetByIIN([FromQuery] string IIN)
    {
        var specification = new UserSpecification(IIN, null);
        var query = new GetUserByIdAsync(specification);
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);

    }
    
    [AuthorizeByRole(AppConstant.AdminRoleName)]
    [HttpGet]
    [ProducesResponseType(typeof(ResponseDTO<UserDTO>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<UserDTO>> Get([FromQuery] UserParameter parameter)
    {
        var specification = new UserSpecification(parameter);
        var query = new GetUserWithSpecAsyncQuery(specification);
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);

    }

    
    [AuthorizeByRole(AppConstant.AdminRoleName)]
    [HttpGet]
    [ProducesResponseType(typeof(Pagination<ICollection<UserDTO>>), (int)HttpStatusCode.OK)]

    public async Task<ActionResult<Pagination<ICollection<UserDTO>>>> All([FromQuery] UserParameter parameter)
    {
        var specification = new UserSpecification(parameter);
        var total = await _mediator.Send(new CountUserAsyncQuery(new CountUserSpecification(parameter)));
        var query = new ListUserWithSpecAsyncQuery(specification);
        var users =  await _mediator.Send(query);
        var pagination = new Pagination<UserDTO>(
            parameter.PageIndex,
            parameter.PageSize,
            total,
            users.Data
        );   
        return Ok(pagination);
    }


    [AuthorizeByRole(AppConstant.AdminRoleName)]
    [HttpPost]
    [ProducesResponseType(typeof(ResponseDTO<UserDTO>), (int)HttpStatusCode.Created)]
    public async Task<ActionResult<ResponseDTO<UserDTO>>> Create([FromBody] UserCreateDTO model)
    {
        var result = await _mediator.Send(new AddUserCommand(model));
        return StatusCode(result.StatusCode,result);

    }

    [AuthorizeByRole(AppConstant.AdminRoleName)]
    [HttpPut]
    [ProducesResponseType(typeof(ResponseDTO<UserDTO>), (int)HttpStatusCode.Created)]
    public async Task<ActionResult<ResponseDTO<UserDTO>>> Update([FromBody] UserUpdateDTO model,long Id)
    {
        var result = await _mediator.Send(new UpdateUserCommand(Id,model));
        return StatusCode(result.StatusCode,result);

    }


}
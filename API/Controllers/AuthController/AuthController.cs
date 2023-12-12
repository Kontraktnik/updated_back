using System.Net;
using System.Security.Claims;
using Application.DTO.Auth;
using Application.DTO.Common;
using Application.DTO.User;
using Application.Features.AuthCQRS.Command.RegisterAsync;
using Application.Features.AuthCQRS.Command.VerifyAsync;
using Application.Features.AuthCQRS.Query.EcpLoginAsync;
using Application.Features.AuthCQRS.Query.LoginAsync;
using Application.Features.UserCQRS.Query.GetUserByIdAsync;
using Application.Resource;
using Infrastracture.Contracts.Specifications.UserSpecification;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace API.Controllers.AuthController;

public class AuthController : BaseApiController
{
    private readonly IMediator _mediator;
    private readonly IStringLocalizer<Localize> _stringLocalizer;
    

    public AuthController(IMediator mediator,IStringLocalizer<Localize> stringLocalizer)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _stringLocalizer = stringLocalizer;
    }
    
    [HttpPost]
    [ProducesResponseType(typeof(AuthResponse<bool>), (int)HttpStatusCode.Created)]
    public async Task<ActionResult<AuthResponse<bool>>> RegisterAsync([FromBody] RegisterDTO registerDto)
    {
        var query = new RegisterAsyncCommand(registerDto);
        var result =  await _mediator.Send(query);
        return StatusCode(result.StatusCode, result);
    }
    
    [HttpPost]
    [ProducesResponseType(typeof(AuthResponse<TokenDTO>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<AuthResponse<TokenDTO>>> LoginAsync([FromBody] LoginDTO loginDto)
    {
        var query = new LoginAsyncQuery(loginDto);
        var result = await _mediator.Send(query);
        return StatusCode(result.StatusCode, result);
    }
    
    
    [HttpPost("ByEcp")]
    [ProducesResponseType(typeof(AuthResponse<TokenDTO>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<AuthResponse<TokenDTO>>> LoginAsync([FromBody] EcpLoginDTO loginDto)
    {
        var query = new EcpLoginAsyncQuery(loginDto);
        var result = await _mediator.Send(query);
        return StatusCode(result.StatusCode, result);
    }
    
    [HttpPost]
    [ProducesResponseType(typeof(AuthResponse<bool>), (int)HttpStatusCode.Created)]
    public async Task<ActionResult<AuthResponse<TokenDTO>>> Verify(VerifyRegistrationDTO verifyRegistrationDto)
    {
        var query = new VerifyAsyncCommand(verifyRegistrationDto);
        var result = await _mediator.Send(query);
        return StatusCode(result.StatusCode, result);
        
    }
    
    [Authorize]
    [HttpGet]
    [ProducesResponseType(typeof(ResponseDTO<UserDTO>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ResponseDTO<UserDTO>>> GetMe()
    {
        var result =  await  _mediator.Send(new GetUserByIdAsync(new UserSpecification(User.FindFirst(ClaimTypes.NameIdentifier)?.Value,null)));
        return StatusCode(result.StatusCode, result);
    }
    
    
    
    
    
    
    
    
}
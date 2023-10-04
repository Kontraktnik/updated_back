using System.Net;
using System.Security.Claims;
using Application.DTO.Auth;
using Application.DTO.Common;
using Application.Features.PhoneNotificationCQRS.Command;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.NotificationController;

public class PhoneNotificationController : BaseApiController
{
    
    private readonly IMediator _mediator;

    public PhoneNotificationController(IMediator mediator)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    }
    
    [HttpPost]
    [ProducesResponseType(typeof(ResponseDTO<bool>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ResponseDTO<bool>>> SendConfCode(string IIN)
    {
        var query = new sendUserConfirmationCodeAgainCommand(IIN);
        var result = await _mediator.Send(query);
        return StatusCode(result.StatusCode,result);

    }
    
    
}
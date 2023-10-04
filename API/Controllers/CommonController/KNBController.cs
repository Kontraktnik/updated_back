using System.Security.Claims;
using API.Helpers;
using API.Model;
using Application.Contracts.Service;
using Application.DTO.Common;
using Application.DTO.Profile;
using Application.DTO.Survey;
using Application.Features.ProfileCQRS.Command.SendConfirmation;
using Application.Features.ProfileCQRS.Query.CountProfileAsync;
using Application.Features.ProfileCQRS.Query.ListProfileWithSpecAsync;
using Application.Features.SurveyCQRS.Query.GetSurveyForEmailWithSpecAsync;
using Application.Features.UserCQRS.Query.GetUserByIdAsync;
using Infrastracture.Contracts.Parameters.ProfileParameters;
using Infrastracture.Contracts.Specifications.ProfileSpecification;
using Infrastracture.Contracts.Specifications.SurveySpecification;
using Infrastracture.Contracts.Specifications.UserSpecification;
using Infrastracture.Helpers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.CommonController;

public class KNBController : BaseApiController
{
    private readonly IMediator _mediator;
    private readonly IEmailService _emailService;
    private readonly IPhoneNotification _phoneNotification;

    public KNBController(IMediator mediator,IEmailService emailService,IPhoneNotification phoneNotification)
    {
        _mediator = mediator;
        _emailService = emailService;
        _phoneNotification = phoneNotification;
    }
    
    [AuthorizeByRole(AppConstant.KNBRoleName)]
    [HttpPost]
    public async Task<ActionResult<ResponseDTO<ProfileDTO>>> SpecialCheckDocuments([FromBody] SendConfirmationDTO model,long ProfileId)
    {
        var user =    await  _mediator.Send(new GetUserByIdAsync(new UserSpecification(User.FindFirst(ClaimTypes.NameIdentifier)?.Value,null)));
        var result = await _mediator.Send(new SendConfirmationCommand(ProfileId,user.Data,model));
        if (result.Data != null)
        {
            SurveyDTO surveyDTO = (await _mediator.Send(new GetSurveyForEmailWithSpecAsyncQuery(new SurveyMailSpecification(result.Data.SurveyId)))).Data;
             _emailService.SendNotificationEmail(surveyDTO);
            await _phoneNotification.SendNotificationByPhone(surveyDTO);
        }
        return StatusCode(result.StatusCode,result);

    }
    
    
    [AuthorizeByRole(AppConstant.KNBRoleName)]
    [HttpGet]
    public async Task<ActionResult<Pagination<ProfileDTO>>> Sended([FromQuery]int pageIndex = 1,int PageSize = 50)
    {
        var user =    await  _mediator.Send(new GetUserByIdAsync(new UserSpecification(User.FindFirst(ClaimTypes.NameIdentifier)?.Value,null)));
        var parameter = new KNBProfileParameters()
        {
            AreaId = user.Data.AreaId,
            Status = 0,
            ConfirmedUserId = null,
            StepId = AppConstant.SpecialState,
            RequestedStatus = 1,
            PageIndex = pageIndex,
            PageSize = PageSize
        };
        var specification = new KNBProfileSpecification(parameter);
        var result = await _mediator.Send(new ListProfileWithSpecAsyncQuery(specification));
        var pagination = new Pagination<ProfileDTO>(
            pageIndex,
            PageSize,
            (await _mediator.Send(new CountProfileAsyncQuery(new KNBProfileCountSpecification(parameter)))),
            result.Data
        );
        return Ok(pagination);
    }
    
    [AuthorizeByRole(AppConstant.KNBRoleName)]
    [HttpGet]
    public async Task<ActionResult<Pagination<ProfileDTO>>> Accepted([FromQuery]int pageIndex = 1,int PageSize = 50)
    {
        var user =    await  _mediator.Send(new GetUserByIdAsync(new UserSpecification(User.FindFirst(ClaimTypes.NameIdentifier)?.Value,null)));
        var parameter = new KNBProfileParameters()
        {
            AreaId = user.Data.AreaId,
            Status = 1,
            ConfirmedUserId = user.Data.Id,
            StepId = AppConstant.SpecialState,
            PageIndex = pageIndex,
            PageSize = PageSize
        };
        var specification = new KNBProfileSpecification(parameter);
        var result = await _mediator.Send(new ListProfileWithSpecAsyncQuery(specification));
        var pagination = new Pagination<ProfileDTO>(
            pageIndex,
            PageSize,
            (await _mediator.Send(new CountProfileAsyncQuery(new KNBProfileCountSpecification(parameter)))),
            result.Data
        );
        return Ok(pagination);
    }
    
    [AuthorizeByRole(AppConstant.KNBRoleName)]
    [HttpGet]
    public async Task<ActionResult<Pagination<ProfileDTO>>> Rejected([FromQuery]int pageIndex = 1,int PageSize = 50)
    {
        var user =    await  _mediator.Send(new GetUserByIdAsync(new UserSpecification(User.FindFirst(ClaimTypes.NameIdentifier)?.Value,null)));
        var parameter = new KNBProfileParameters()
        {
            AreaId = user.Data.AreaId,
            Status = -1,
            ConfirmedUserId = user.Data.Id,
            StepId = AppConstant.SpecialState,
            PageIndex = pageIndex,
            PageSize = PageSize
        };
        var specification = new KNBProfileSpecification(parameter);
        var result = await _mediator.Send(new ListProfileWithSpecAsyncQuery(specification));
        var pagination = new Pagination<ProfileDTO>(
            pageIndex,
            PageSize,
            (await _mediator.Send(new CountProfileAsyncQuery(new KNBProfileCountSpecification(parameter)))),
            result.Data
        );
        return Ok(pagination);
    }

    


}
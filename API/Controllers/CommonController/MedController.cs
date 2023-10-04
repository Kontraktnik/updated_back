using System.Security.Claims;
using API.Helpers;
using API.Model;
using Application.Contracts.Service;
using Application.DTO.Common;
using Application.DTO.Profile;
using Application.Features.ProfileCQRS.Command.SendConfirmation;
using Application.Features.ProfileCQRS.Query.CountProfileAsync;
using Application.Features.ProfileCQRS.Query.ListProfileWithSpecAsync;
using Application.Features.SurveyCQRS.Query.GetSurveyForEmailWithSpecAsync;
using Application.Features.UserCQRS.Query.GetUserByIdAsync;
using Domain.Models.NotificationModels;
using Infrastracture.Contracts.Parameters.ProfileParameters;
using Infrastracture.Contracts.Specifications.ProfileSpecification;
using Infrastracture.Contracts.Specifications.SurveySpecification;
using Infrastracture.Contracts.Specifications.UserSpecification;
using Infrastracture.Helpers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.CommonController;

public class MedController : BaseApiController
{
    private readonly IMediator _mediator;
    private readonly IEmailService _emailService;
    private readonly IPhoneNotification _phoneNotification;

    public MedController(IMediator mediator,IEmailService emailService,IPhoneNotification phoneNotification)
    {
        _mediator = mediator;
        _emailService = emailService;
        _phoneNotification = phoneNotification;
    }
    
    
    [AuthorizeByRole(AppConstant.MEDRoleName)]
    [HttpPost]
    public async Task<ActionResult<ResponseDTO<ProfileDTO>>> MedCheckProfile([FromBody] SendConfirmationDTO model,long ProfileId)
    {
        var user =    await  _mediator.Send(new GetUserByIdAsync(new UserSpecification(User.FindFirst(ClaimTypes.NameIdentifier)?.Value,null)));
        var result = await _mediator.Send(new SendConfirmationCommand(ProfileId,user.Data,model));
        if (result.Data != null)
        {
             _emailService.SendNotificationEmail((await _mediator.Send(new GetSurveyForEmailWithSpecAsyncQuery(new SurveyMailSpecification(result.Data.SurveyId)))).Data);
             _phoneNotification.SendNotificationByPhone((await _mediator.Send(new GetSurveyForEmailWithSpecAsyncQuery(new SurveyMailSpecification(result.Data.SurveyId)))).Data);
        }
        return StatusCode(result.StatusCode,result);

    }
    
    
    [AuthorizeByRole(AppConstant.MEDRoleName)]
    [HttpPost]
    public async Task<ActionResult<ResponseDTO<ProfileDTO>>> CheckUserPsycho([FromBody] SendConfirmationDTO model,long ProfileId)
    {
        var user =    await  _mediator.Send(new GetUserByIdAsync(new UserSpecification(User.FindFirst(ClaimTypes.NameIdentifier)?.Value,null)));
        var result = await _mediator.Send(new SendConfirmationCommand(ProfileId,user.Data,model));
        if (result.Data != null)
        {
             _emailService.SendNotificationEmail((await _mediator.Send(new GetSurveyForEmailWithSpecAsyncQuery(new SurveyMailSpecification(result.Data.SurveyId)))).Data);
             _phoneNotification.SendNotificationByPhone((await _mediator.Send(new GetSurveyForEmailWithSpecAsyncQuery(new SurveyMailSpecification(result.Data.SurveyId)))).Data);
        }
        return StatusCode(result.StatusCode,result);

    }
    
    
    [AuthorizeByRole(AppConstant.MEDRoleName)]
    [HttpGet]
    public async Task<ActionResult<Pagination<ProfileDTO>>> Sended([FromQuery]int pageIndex = 1,int PageSize = 50)
    {
        var user =    await  _mediator.Send(new GetUserByIdAsync(new UserSpecification(User.FindFirst(ClaimTypes.NameIdentifier)?.Value,null)));
        var parameter = new MedProfileParameters()
        {
            AreaId = user.Data.AreaId,
            Status = 0,
            ConfirmedUserId = null,
            StepId = new List<long?>()
            {
                AppConstant.MedState,
            },
            PageIndex = pageIndex,
            PageSize = PageSize
        };
        var specification = new MedProfileSpecification(parameter);
        var result = await _mediator.Send(new ListProfileWithSpecAsyncQuery(specification));
        var pagination = new Pagination<ProfileDTO>(
            pageIndex,
            PageSize,
            (await _mediator.Send(new CountProfileAsyncQuery(new MedProfileCountSpecification(parameter)))),
            result.Data
        );
        return Ok(pagination);
    }
   
    [AuthorizeByRole(AppConstant.MEDRoleName)]
    [HttpGet]
    public async Task<ActionResult<Pagination<ProfileDTO>>> SendedPsycho([FromQuery]int pageIndex = 1,int PageSize = 50)
    {
        var user =    await  _mediator.Send(new GetUserByIdAsync(new UserSpecification(User.FindFirst(ClaimTypes.NameIdentifier)?.Value,null)));
        var parameter = new MedProfileParameters()
        {
            AreaId = user.Data.AreaId,
            Status = 0,
            ConfirmedUserId = null,
            StepId = new List<long?>()
            {
                AppConstant.PsychoMedState
            },
            PageIndex = pageIndex,
            PageSize = PageSize
        };
        var specification = new MedProfileSpecification(parameter);
        var result = await _mediator.Send(new ListProfileWithSpecAsyncQuery(specification));
        var pagination = new Pagination<ProfileDTO>(
            pageIndex,
            PageSize,
            (await _mediator.Send(new CountProfileAsyncQuery(new MedProfileCountSpecification(parameter)))),
            result.Data
        );
        return Ok(pagination);
    }

    
    
    [AuthorizeByRole(AppConstant.MEDRoleName)]
    [HttpGet]
    public async Task<ActionResult<Pagination<ProfileDTO>>> Accepted([FromQuery]int pageIndex = 1,int PageSize = 50)
    {
        var user =    await  _mediator.Send(new GetUserByIdAsync(new UserSpecification(User.FindFirst(ClaimTypes.NameIdentifier)?.Value,null)));
        var parameter = new MedProfileParameters()
        {
            AreaId = user.Data.AreaId,
            Status = 1,
            ConfirmedUserId = user.Data.Id,
            StepId =  new List<long?>()
            {
                AppConstant.MedState,
                AppConstant.PsychoMedState
            },
            PageIndex = pageIndex,
            PageSize = PageSize
        };
        var specification = new MedProfileSpecification(parameter);
        var result = await _mediator.Send(new ListProfileWithSpecAsyncQuery(specification));
        var pagination = new Pagination<ProfileDTO>(
            pageIndex,
            PageSize,
            (await _mediator.Send(new CountProfileAsyncQuery(new MedProfileCountSpecification(parameter)))),
            result.Data
        );
        return Ok(pagination);
    }
    
    [AuthorizeByRole(AppConstant.MEDRoleName)]
    [HttpGet]
    public async Task<ActionResult<Pagination<ProfileDTO>>> Rejected([FromQuery]int pageIndex = 1,int PageSize = 50)
    {
        var user =    await  _mediator.Send(new GetUserByIdAsync(new UserSpecification(User.FindFirst(ClaimTypes.NameIdentifier)?.Value,null)));
        var parameter = new MedProfileParameters()
        {
            AreaId = user.Data.AreaId,
            Status = -1,
            ConfirmedUserId = user.Data.Id,
            StepId  = new List<long?>()
            {
                AppConstant.MedState,
                AppConstant.PsychoMedState
            },
            PageIndex = pageIndex,
            PageSize = PageSize
        };
        var specification = new MedProfileSpecification(parameter);
        var result = await _mediator.Send(new ListProfileWithSpecAsyncQuery(specification));
        var pagination = new Pagination<ProfileDTO>(
            pageIndex,
            PageSize,
            (await _mediator.Send(new CountProfileAsyncQuery(new MedProfileCountSpecification(parameter)))),
            result.Data
        );
        return Ok(pagination);
    }
    
    
    
}
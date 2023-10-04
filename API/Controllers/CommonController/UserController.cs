using System.Security.Claims;
using API.Helpers;
using API.Model;
using Application.Contracts.Service;
using Application.DTO.Common;
using Application.DTO.Profile;
using Application.DTO.Survey;
using Application.Features.ProfileCQRS.Command.SendConfirmation;
using Application.Features.ProfileCQRS.Command.SendRequest;
using Application.Features.SurveyCQRS.Command.AddSurvey;
using Application.Features.SurveyCQRS.Query.CountSurveyAsync;
using Application.Features.SurveyCQRS.Query.GetSurveyForEmailWithSpecAsync;
using Application.Features.SurveyCQRS.Query.GetSurveyWithSpecAsync;
using Application.Features.SurveyCQRS.Query.ListSurveyWithSpecAsync;
using Application.Features.UserCQRS.Query.GetUserByIdAsync;
using Infrastracture.Contracts.Specifications.SurveySpecification;
using Infrastracture.Contracts.Specifications.UserSpecification;
using Infrastracture.Helpers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.CommonController;

public class UserController : BaseApiController
{
    
    private readonly IMediator _mediator;
    private readonly IEmailService _emailService;

    public UserController(IMediator mediator, IEmailService emailService)
    {
        _mediator = mediator;
        _emailService = emailService;
    }
    
    //Отправка документов - Request
    [AuthorizeByRole(AppConstant.UserRoleName)]
    [HttpPost]
    public async Task<ActionResult<ResponseDTO<ProfileDTO>>> SendRequest([FromBody] SurveyCUDTO model,string UserSign)
    {
        var survey =  await _mediator.Send(new AddSurveyCommand(model, User.FindFirst(ClaimTypes.NameIdentifier)?.Value,0));
        
        if (survey != null)
        {
            var user =    await  _mediator.Send(new GetUserByIdAsync(new UserSpecification(User.FindFirst(ClaimTypes.NameIdentifier)?.Value,null)));
            var requestDto = new SendRequestDTO() { Status = 1, SignKey = UserSign, StepId = AppConstant.SendedState, SurveyId = survey.Data.Id };
            var result = await _mediator.Send(new SendRequestCommand(user.Data, requestDto));
            if (result.Data != null)
            {
                 _emailService.SendNotificationEmail((await _mediator.Send(new GetSurveyForEmailWithSpecAsyncQuery(new SurveyMailSpecification(result.Data.SurveyId)))).Data);
            }
            return StatusCode(result.StatusCode,result);

        }
        return BadRequest();
    }
    
    [AuthorizeByRole(AppConstant.UserRoleName)]
    [HttpPost]
    public async Task<ActionResult<ResponseDTO<ProfileDTO>>> SendSavedRequest([FromBody] long SurveyId,string UserSign)
    {
        var user =    await  _mediator.Send(new GetUserByIdAsync(new UserSpecification(User.FindFirst(ClaimTypes.NameIdentifier)?.Value,null)));
        if (user.Data != null)
        {
            var requestDto = new SendRequestDTO
            {
                Status = 1,
                SignKey = UserSign,
                StepId = 1,
                SurveyId = SurveyId
            };
            var result = await _mediator.Send(new SendRequestCommand(user.Data, requestDto));
            if (result.Data != null)
            {
                 _emailService.SendNotificationEmail((await _mediator.Send(new GetSurveyForEmailWithSpecAsyncQuery(new SurveyMailSpecification(result.Data.SurveyId)))).Data);
            }
            return StatusCode(result.StatusCode,result);

        }
        return BadRequest();
    }
    //Принятие медобследования - Confirm
    [AuthorizeByRole(AppConstant.UserRoleName)]
    [HttpPost]
    public async Task<ActionResult<ResponseDTO<ProfileDTO>>> SendMedDocuments([FromBody] SendConfirmationDTO model,long ProfileId)
    {
        var user =    await  _mediator.Send(new GetUserByIdAsync(new UserSpecification(User.FindFirst(ClaimTypes.NameIdentifier)?.Value,null)));
        var result = await _mediator.Send(new SendConfirmationCommand(ProfileId,user.Data,model));
        if (result.Data != null)
        {
             _emailService.SendNotificationEmail((await _mediator.Send(new GetSurveyForEmailWithSpecAsyncQuery(new SurveyMailSpecification(result.Data.SurveyId)))).Data);
        }
        return StatusCode(result.StatusCode,result);

    }
    
    //Принятие психологического тестирования - Confirm
    [AuthorizeByRole(AppConstant.UserRoleName)]
    [HttpPost]
    public async Task<ActionResult<ResponseDTO<ProfileDTO>>> PassPsycho([FromBody] SendConfirmationDTO model,long ProfileId)
    {
        var user =    await  _mediator.Send(new GetUserByIdAsync(new UserSpecification(User.FindFirst(ClaimTypes.NameIdentifier)?.Value,null)));
        var result = await _mediator.Send(new SendConfirmationCommand(ProfileId,user.Data,model));

        if (result.Data != null)
        {
             _emailService.SendNotificationEmail((await _mediator.Send(new GetSurveyForEmailWithSpecAsyncQuery(new SurveyMailSpecification(result.Data.SurveyId)))).Data);
        }
        return StatusCode(result.StatusCode,result);

    }
    
    //Принятие предложения - Confirm
    [AuthorizeByRole(AppConstant.UserRoleName)]
    [HttpPost]
    public async Task<ActionResult<ResponseDTO<ProfileDTO>>> SignOffer([FromBody] SendConfirmationDTO model,long ProfileId)
    {
        var user =    await  _mediator.Send(new GetUserByIdAsync(new UserSpecification(User.FindFirst(ClaimTypes.NameIdentifier)?.Value,null)));
        var result = await _mediator.Send(new SendConfirmationCommand(ProfileId,user.Data,model));
        if (result.Data != null)
        {
            await _emailService.SendNotificationEmail((await _mediator.Send(new GetSurveyForEmailWithSpecAsyncQuery(new SurveyMailSpecification(result.Data.SurveyId)))).Data);
        }
        return StatusCode(result.StatusCode,result);

    }
    
    [AuthorizeByRole(AppConstant.UserRoleName)]
    [HttpGet]
    public async Task<ActionResult<Pagination<ProfileDTO>>> MySurveys(int PageIndex = 1,int PageSize = 20)
    {
        var user =    await  _mediator.Send(new GetUserByIdAsync(new UserSpecification(User.FindFirst(ClaimTypes.NameIdentifier)?.Value,null)));
        var result = await _mediator.Send(new ListSurveyWithSpecAsyncQuery(new SurveySpecification(user.Data,PageIndex,PageSize,true),user.Data));
        var pagination = new Pagination<SurveyDTO>(
            PageIndex,
            PageSize,
            (await _mediator.Send(new CountSurveyAsyncQuery(new SurveySpecification(user.Data,PageIndex,PageSize,false)))),
            result.Data

        );
        return Ok(pagination);
    }
    
    
    
    
    
    
    
}
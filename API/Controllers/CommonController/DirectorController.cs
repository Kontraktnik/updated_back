using System.Security.Claims;
using API.Helpers;
using API.Model;
using Application.Contracts.Service;
using Application.DTO.Common;
using Application.DTO.Profile;
using Application.DTO.Survey;
using Application.DTO.User;
using Application.Features.ProfileCQRS.Command.SendConfirmation;
using Application.Features.ProfileCQRS.Query.CountProfileAsync;
using Application.Features.ProfileCQRS.Query.ListProfileWithSpecAsync;
using Application.Features.SurveyCQRS.Query.CountSurveyAsync;
using Application.Features.SurveyCQRS.Query.GetSurveyForEmailWithSpecAsync;
using Application.Features.SurveyCQRS.Query.GetSurveyStatsWithSpecAsync;
using Application.Features.SurveyCQRS.Query.GetSurveyWithSpecAsync;
using Application.Features.SurveyCQRS.Query.ListSurveyWithSpecAsync;
using Application.Features.SurveyExecutorCQRS.Command.AddSurveyExecutor;
using Application.Features.SurveyExecutorCQRS.Query.GetSurveyListByRole;
using Application.Features.UserCQRS.Query.GetUserByIdAsync;
using Application.Features.UserCQRS.Query.ListUserWithSpecAsync;
using Domain.Models.NotificationModels;
using Domain.Models.SurveyModels;
using Infrastracture.Contracts.Parameters.ProfileParameters;
using Infrastracture.Contracts.Parameters.SurveyRepositories;
using Infrastracture.Contracts.Specifications.ProfileSpecification;
using Infrastracture.Contracts.Specifications.SurveySpecification;
using Infrastracture.Contracts.Specifications.UserSpecification;
using Infrastracture.Helpers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.CommonController;

public class DirectorController : BaseApiController
{
    private readonly IMediator _mediator;
    private readonly IEmailService _emailService;
    private readonly IPhoneNotification _phoneNotification;

    public DirectorController(IMediator mediator,IEmailService emailService,IPhoneNotification phoneNotification)
    {
        _mediator = mediator;
        _emailService = emailService;
        _phoneNotification = phoneNotification;

    }
    
    //Руководитель распределяет работу - Confirm
    [AuthorizeByRole(AppConstant.DirectorName)]
    [HttpPost]
    public async Task<ActionResult<ResponseDTO<ProfileDTO>>> ConfirmSendedDocument([FromBody] SurveyExecutorCUDTO model,long ProfileId,string SignKey)
    {
        var user =    await  _mediator.Send(new GetUserByIdAsync(new UserSpecification(User.FindFirst(ClaimTypes.NameIdentifier)?.Value,null)));
        model.DirectorId = user.Data.Id;
        var executor = await _mediator.Send(new AddSurveyExecutorCommand(model));
        if (executor.Success)
        {
            SendConfirmationDTO confirmationDto = new SendConfirmationDTO()
            {
                SignKey = SignKey,
                Status = 1
            };
            var result = await _mediator.Send(new SendConfirmationCommand(ProfileId, user.Data,confirmationDto));
            if (result.Data != null)
            {
                 _emailService.SendNotificationEmail((await _mediator.Send(new GetSurveyForEmailWithSpecAsyncQuery(new SurveyMailSpecification(result.Data.SurveyId)))).Data);
                 _phoneNotification.SendNotificationByPhone((await _mediator.Send(new GetSurveyForEmailWithSpecAsyncQuery(new SurveyMailSpecification(result.Data.SurveyId)))).Data);
            }
            return StatusCode(result.StatusCode,result);

        }
        return BadRequest(executor.Message);
    }
    
    
    //Финальный этап - руководитель подтверждает принятие на работу - confirm
    [AuthorizeByRole(AppConstant.DirectorName)]
    [HttpPost]
    public async Task<ActionResult<ResponseDTO<ProfileDTO>>> SignUser([FromBody] SendConfirmationDTO model,long ProfileId)
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
    
    //Пришедшие
    [AuthorizeByRole(AppConstant.DirectorName)]
    [HttpGet]
    public async Task<ActionResult<Pagination<ProfileDTO>>> Sended([FromQuery]int pageIndex = 1,int PageSize = 50)
    {
        var user =    await  _mediator.Send(new GetUserByIdAsync(new UserSpecification(User.FindFirst(ClaimTypes.NameIdentifier)?.Value,null)));
        var parameter = new DirectorProfileParameters()
        {
            //AreaId = user.Data.AreaId,
            Status = 0,
            ConfirmedUserId = null,
            StepId = AppConstant.SendedState,
            PageIndex = pageIndex,
            PageSize = PageSize
        };
        var specification = new DirectorProfileSpecification(parameter);
        var result = await _mediator.Send(new ListProfileWithSpecAsyncQuery(specification));
        var pagination = new Pagination<ProfileDTO>(
            pageIndex,
            PageSize,
            (await _mediator.Send(new CountProfileAsyncQuery(new DirectorProfileSpecification(parameter,false)))),
            result.Data
        );
        return Ok(pagination);
    }
    //На подписание
    [AuthorizeByRole(AppConstant.DirectorName)]
    [HttpGet]
    public async Task<ActionResult<Pagination<ProfileDTO>>> Offered([FromQuery]int pageIndex = 1,int PageSize = 50)
    {
        var user =    await  _mediator.Send(new GetUserByIdAsync(new UserSpecification(User.FindFirst(ClaimTypes.NameIdentifier)?.Value,null)));
        var parameter = new DirectorProfileParameters()
        {
            //reaId = user.Data.AreaId,
            Status = 0,
            ConfirmedUserId = user.Data.Id,
            StepId = AppConstant.SigningState,
            PageIndex = pageIndex,
            PageSize = PageSize
        };
        var specification = new DirectorProfileSpecification(parameter);
        var result = await _mediator.Send(new ListProfileWithSpecAsyncQuery(specification));
        var pagination = new Pagination<ProfileDTO>(
            pageIndex,
            PageSize,
            (await _mediator.Send(new CountProfileAsyncQuery(specification))),
            result.Data
        );
        return Ok(pagination);
    }
    
    //Подписанные
    [AuthorizeByRole(AppConstant.DirectorName)]
    [HttpGet]
    public async Task<ActionResult<Pagination<ProfileDTO>>> Signed([FromQuery]int pageIndex = 1,int PageSize = 50)
    {
        var user =    await  _mediator.Send(new GetUserByIdAsync(new UserSpecification(User.FindFirst(ClaimTypes.NameIdentifier)?.Value,null)));
        var parameter = new DirectorProfileParameters()
        {
            //AreaId = user.Data.AreaId,
            Status = 1,
            ConfirmedUserId = user.Data.Id,
            StepId = AppConstant.SigningState,
            PageIndex = pageIndex,
            PageSize = PageSize
        };
        var specification = new DirectorProfileSpecification(parameter);
        var result = await _mediator.Send(new ListProfileWithSpecAsyncQuery(specification));
        var pagination = new Pagination<ProfileDTO>(
            pageIndex,
            PageSize,
            (await _mediator.Send(new CountProfileAsyncQuery(new DirectorProfileCountSpecification(parameter)))),
            result.Data
        );
        return Ok(pagination);
    }
    //Отказанные

    [AuthorizeByRole(AppConstant.DirectorName)]
    [HttpGet]
    public async Task<ActionResult<Pagination<ProfileDTO>>> Rejected([FromQuery]int pageIndex = 1,int PageSize = 50)
    {
        var user =    await  _mediator.Send(new GetUserByIdAsync(new UserSpecification(User.FindFirst(ClaimTypes.NameIdentifier)?.Value,null)));
        var parameter = new DirectorProfileParameters()
        {
            //AreaId = user.Data.AreaId,
            Status = -1,
            ConfirmedUserId = null,
            StepId = null,
            PageIndex = pageIndex,
            PageSize = PageSize
        };
        var specification = new DirectorProfileSpecification(parameter);
        var result = await _mediator.Send(new ListProfileWithSpecAsyncQuery(specification));
        var pagination = new Pagination<ProfileDTO>(
            pageIndex,
            PageSize,
            (await _mediator.Send(new CountProfileAsyncQuery(new DirectorProfileCountSpecification(parameter)))),
            result.Data
        );
        return Ok(pagination);
    }
    
    //Получить список исполнителей по региону

    [AuthorizeByRole(AppConstant.DirectorName)]
    [HttpGet]
    public async Task<ActionResult<ResponseDTO<ICollection<UserDTO>>>> GetExecutors()
    {
        var result = await _mediator.Send(new ListUserWithSpecAsyncQuery(new ExecutorSpecification()));
        return StatusCode(result.StatusCode,result);
    }
    
    //Статус
    [AuthorizeByRole(AppConstant.DirectorName)]
    [HttpGet]
    public async Task<ActionResult<ResponseDTO<ICollection<UserDTO>>>> GetStats([FromQuery] SurveyStatsParameters parameters)
    {
        var user =    await  _mediator.Send(new GetUserByIdAsync(new UserSpecification(User.FindFirst(ClaimTypes.NameIdentifier)?.Value,null)));
        var result = await _mediator.Send(new GetSurveyStatsWithSpecAsyncQuery(new SurveyStatsSpecification(parameters),parameters.ExecutorId));
        return StatusCode(result.StatusCode,result);
    }

    //Мои заявки

    [AuthorizeByRole(AppConstant.DirectorName)]
    [HttpGet]
    public async Task<ActionResult<Pagination<Survey>>> MySurveys([FromQuery] SurveyForManagementParameters parameters)
    {
        var user =    await  _mediator.Send(new GetUserByIdAsync(new UserSpecification(User.FindFirst(ClaimTypes.NameIdentifier)?.Value,null)));
        List<long> SurveysId = await _mediator.Send(new GetSurveyListByRoleQuery(user.Data));
        var result = await _mediator.Send(new ListSurveyWithSpecAsyncQuery(new SurveyForManagementSpecification(parameters, SurveysId, true),user.Data));
        if (result.Success)
        {
            var pagination = new Pagination<SurveyDTO>(
                parameters.PageIndex,
                parameters.PageSize,
                (await _mediator.Send(new CountSurveyAsyncQuery(new SurveyForManagementSpecification(parameters, SurveysId, false)))),
                result.Data
            );
            return StatusCode(result.StatusCode, pagination);

        }

        return StatusCode(result.StatusCode, result);

    }
    



}
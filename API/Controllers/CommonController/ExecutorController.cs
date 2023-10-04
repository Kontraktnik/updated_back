using System.Security.Claims;
using API.Helpers;
using API.Model;
using Application.Contracts.Persistence;
using Application.Contracts.Service;
using Application.DTO.Common;
using Application.DTO.Profile;
using Application.DTO.Survey;
using Application.Features.ProfileCQRS.Command.SendConfirmation;
using Application.Features.ProfileCQRS.Command.SendRequest;
using Application.Features.ProfileCQRS.Query.CountProfileAsync;
using Application.Features.ProfileCQRS.Query.ListProfileWithSpecAsync;
using Application.Features.SurveyCQRS.Query.CountSurveyAsync;
using Application.Features.SurveyCQRS.Query.GetSurveyForEmailWithSpecAsync;
using Application.Features.SurveyCQRS.Query.ListSurveyWithSpecAsync;
using Application.Features.SurveyExecutorCQRS.Query.GetSurveyListByRole;
using Application.Features.UserCQRS.Query.GetUserByIdAsync;
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

public class ExecutorController : BaseApiController
{
    private readonly IMediator _mediator;
    private readonly ISurveyExecutorRepository _surveyExecutorRepository;
    private readonly IEmailService _emailService;
    private readonly IPhoneNotification _phoneNotification;

    public ExecutorController(IMediator mediator,ISurveyExecutorRepository surveyExecutorRepository,IEmailService emailService,IPhoneNotification phoneNotification)
    {
        _mediator = mediator;
        _surveyExecutorRepository = surveyExecutorRepository;
        _emailService = emailService;
        _phoneNotification = phoneNotification;
    }
    //Принятие документа Confirmation
    [AuthorizeByRole(AppConstant.ExecutorName)]
    [HttpPost]
    public async Task<ActionResult<ResponseDTO<ProfileDTO>>> AcceptDocuments([FromBody] SendConfirmationDTO model,long ProfileId)
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
    //Отправка на cпец проверку - Request
    [AuthorizeByRole(AppConstant.ExecutorName)]
    [HttpPost]
    public async Task<ActionResult<ResponseDTO<ProfileDTO>>> SendToKnb([FromBody] SendRequestDTO model)
    {
        var user =    await  _mediator.Send(new GetUserByIdAsync(new UserSpecification(User.FindFirst(ClaimTypes.NameIdentifier)?.Value,null)));
        var result= await _mediator.Send(new SendRequestCommand(user.Data,model));
        if (result.Data != null)
        {
             _emailService.SendNotificationEmail((await _mediator.Send(new GetSurveyForEmailWithSpecAsyncQuery(new SurveyMailSpecification(result.Data.SurveyId)))).Data);
             _phoneNotification.SendNotificationByPhone((await _mediator.Send(new GetSurveyForEmailWithSpecAsyncQuery(new SurveyMailSpecification(result.Data.SurveyId)))).Data);
        }
        return StatusCode(result.StatusCode,result);

    }
    //Отправка на мед обследование - Request
    [AuthorizeByRole(AppConstant.ExecutorName)]
    [HttpPost]
    public async Task<ActionResult<ResponseDTO<ProfileDTO>>> SendToMed([FromBody] SendRequestDTO model)
    {
        var user =    await  _mediator.Send(new GetUserByIdAsync(new UserSpecification(User.FindFirst(ClaimTypes.NameIdentifier)?.Value,null)));
        var result= await _mediator.Send(new SendRequestCommand(user.Data,model));
        if (result.Data != null)
        {
             _emailService.SendNotificationEmail((await _mediator.Send(new GetSurveyForEmailWithSpecAsyncQuery(new SurveyMailSpecification(result.Data.SurveyId)))).Data);
             _phoneNotification.SendNotificationByPhone((await _mediator.Send(new GetSurveyForEmailWithSpecAsyncQuery(new SurveyMailSpecification(result.Data.SurveyId)))).Data);
        }
        return StatusCode(result.StatusCode,result);

    }
    
    //Отправка на псих обследование - Request
    [AuthorizeByRole(AppConstant.ExecutorName)]
    [HttpPost]
    public async Task<ActionResult<ResponseDTO<ProfileDTO>>> SendUserToPsycho([FromBody] SendRequestDTO model)
    {
        var user =    await  _mediator.Send(new GetUserByIdAsync(new UserSpecification(User.FindFirst(ClaimTypes.NameIdentifier)?.Value,null)));
        var result = await _mediator.Send(new SendRequestCommand(user.Data,model));
        if (result.Data != null)
        {
             _emailService.SendNotificationEmail((await _mediator.Send(new GetSurveyForEmailWithSpecAsyncQuery(new SurveyMailSpecification(result.Data.SurveyId)))).Data);
             _phoneNotification.SendNotificationByPhone((await _mediator.Send(new GetSurveyForEmailWithSpecAsyncQuery(new SurveyMailSpecification(result.Data.SurveyId)))).Data);
        }
        return StatusCode(result.StatusCode,result);

    }
    //Отправка предложения - Request
    [AuthorizeByRole(AppConstant.ExecutorName)]
    [HttpPost]
    public async Task<ActionResult<ResponseDTO<ProfileDTO>>> OfferToUser([FromBody] SendRequestDTO model)
    {
        var user =    await  _mediator.Send(new GetUserByIdAsync(new UserSpecification(User.FindFirst(ClaimTypes.NameIdentifier)?.Value,null)));
        var result = await _mediator.Send(new SendRequestCommand(user.Data,model));
        if (result.Data != null)
        {
             _emailService.SendNotificationEmail((await _mediator.Send(new GetSurveyForEmailWithSpecAsyncQuery(new SurveyMailSpecification(result.Data.SurveyId)))).Data);
             _phoneNotification.SendNotificationByPhone((await _mediator.Send(new GetSurveyForEmailWithSpecAsyncQuery(new SurveyMailSpecification(result.Data.SurveyId)))).Data);
        }
        return StatusCode(result.StatusCode,result);

    }
    
    //Отправка предложения на конечное подписание - Request
    [AuthorizeByRole(AppConstant.ExecutorName)]
    [HttpPost]
    public async Task<ActionResult<ResponseDTO<ProfileDTO>>> SendOfferToSign([FromBody] SendRequestDTO model)
    {
        var user =    await  _mediator.Send(new GetUserByIdAsync(new UserSpecification(User.FindFirst(ClaimTypes.NameIdentifier)?.Value,null)));
        var result = await _mediator.Send(new SendRequestCommand(user.Data,model));
        if (result.Data != null)
        {
             _emailService.SendNotificationEmail((await _mediator.Send(new GetSurveyForEmailWithSpecAsyncQuery(new SurveyMailSpecification(result.Data.SurveyId)))).Data);
             _phoneNotification.SendNotificationByPhone((await _mediator.Send(new GetSurveyForEmailWithSpecAsyncQuery(new SurveyMailSpecification(result.Data.SurveyId)))).Data);
        }
        return StatusCode(result.StatusCode,result);

    }
    
    //Поступившие заявки
    [AuthorizeByRole(AppConstant.ExecutorName)]
    [HttpGet]
    public async Task<ActionResult<Pagination<ProfileDTO>>> Sended([FromQuery]int pageIndex = 1,int PageSize = 50)
    {
        var user =    await  _mediator.Send(new GetUserByIdAsync(new UserSpecification(User.FindFirst(ClaimTypes.NameIdentifier)?.Value,null)));
        var parameter = new ExecutorProfileParameter()
        {
            AreaId = user.Data.AreaId,
            Status = new List<int>()
            {
                0
            },
            ConfirmedUserId = user.Data.Id,
            StepId = AppConstant.AcceptedState,
            PageIndex = pageIndex,
            PageSize = PageSize
        };
        var specification = new ExecutorProfileSpecification(parameter);
        var result = await _mediator.Send(new ListProfileWithSpecAsyncQuery(specification));
        var pagination = new Pagination<ProfileDTO>(
            pageIndex,
            PageSize,
            (await _mediator.Send(new CountProfileAsyncQuery(new ExecutorProfileSpecification(parameter,false)))),
            result.Data
        );
        return Ok(pagination);
    }
    //Принятые в работу
    [AuthorizeByRole(AppConstant.ExecutorName)]
    [HttpGet]
    public async Task<ActionResult<Pagination<ProfileDTO>>> Accepted([FromQuery]int pageIndex = 1,int PageSize = 50)
    {
        var user =    await  _mediator.Send(new GetUserByIdAsync(new UserSpecification(User.FindFirst(ClaimTypes.NameIdentifier)?.Value,null)));
        var parameter = new ExecutorProfileParameter()
        {
            AreaId = user.Data.AreaId,
            Status = new List<int>()
            {
                1
            },
            ConfirmedUserId = user.Data.Id,
            StepId = AppConstant.AcceptedState,
            PageIndex = pageIndex,
            PageSize = PageSize
        };
        var specification = new ExecutorProfileSpecification(parameter);
        var result = await _mediator.Send(new ListProfileWithSpecAsyncQuery(specification));
        var pagination = new Pagination<ProfileDTO>(
            pageIndex,
            PageSize,
            (await _mediator.Send(new CountProfileAsyncQuery(new ExecutorProfileSpecification(parameter,false)))),
            result.Data
        );
        return Ok(pagination);
    }
    //На спец проверке
    [AuthorizeByRole(AppConstant.ExecutorName)]
    [HttpGet]
    public async Task<ActionResult<Pagination<ProfileDTO>>> SpecialCheck([FromQuery]int pageIndex = 1,int PageSize = 50,[FromQuery(Name = "RequestedStatus")] List<int> RequestedStatus = null)
    {
        var user =    await  _mediator.Send(new GetUserByIdAsync(new UserSpecification(User.FindFirst(ClaimTypes.NameIdentifier)?.Value,null)));
        var parameter = new ExecutorProfileParameter()
        {
            AreaId = user.Data.AreaId,
            Status = new List<int>()
            {
                -1,0,1
            },
            RequestedStatus = RequestedStatus,
            RequestedUserId = user.Data.Id,
            StepId = AppConstant.SpecialState,
            PageIndex = pageIndex,
            PageSize = PageSize
        };
        var specification = new ExecutorProfileSpecification(parameter);
        var result = await _mediator.Send(new ListProfileWithSpecAsyncQuery(specification));
        var pagination = new Pagination<ProfileDTO>(
            pageIndex,
            PageSize,
            (await _mediator.Send(new CountProfileAsyncQuery(new ExecutorProfileSpecification(parameter,false)))),
            result.Data
        );
        return Ok(pagination);
    }
    
    //Подготовка к медобследованию
    [AuthorizeByRole(AppConstant.ExecutorName)]
    [HttpGet]
    public async Task<ActionResult<Pagination<ProfileDTO>>> MedState([FromQuery]int pageIndex = 1,int PageSize = 50,[FromQuery(Name = "RequestedStatus")] List<int> RequestedStatus = null)
    {
        var user =    await  _mediator.Send(new GetUserByIdAsync(new UserSpecification(User.FindFirst(ClaimTypes.NameIdentifier)?.Value,null)));
        var parameter = new ExecutorProfileParameter()
        {
            AreaId = user.Data.AreaId,
            Status = new List<int>()
            {
                -1,0,1
            },
            RequestedStatus = RequestedStatus,
            RequestedUserId = user.Data.Id,
            StepId = AppConstant.PreparedMedState,
            PageIndex = pageIndex,
            PageSize = PageSize
        };
        var specification = new ExecutorProfileSpecification(parameter);
        var result = await _mediator.Send(new ListProfileWithSpecAsyncQuery(specification));
        var pagination = new Pagination<ProfileDTO>(
            pageIndex,
            PageSize,
            (await _mediator.Send(new CountProfileAsyncQuery(new ExecutorProfileSpecification(parameter,false)))),
            result.Data
        );
        return Ok(pagination);
    }
    
    //Подготовка к психологическому тестированию
    [AuthorizeByRole(AppConstant.ExecutorName)]
    [HttpGet]
    public async Task<ActionResult<Pagination<ProfileDTO>>> PsychoState([FromQuery]int pageIndex = 1,int PageSize = 50,[FromQuery(Name = "RequestedStatus")] List<int> RequestedStatus = null)
    {
        var user =    await  _mediator.Send(new GetUserByIdAsync(new UserSpecification(User.FindFirst(ClaimTypes.NameIdentifier)?.Value,null)));
        var parameter = new ExecutorProfileParameter()
        {
            AreaId = user.Data.AreaId,
            Status = new List<int>()
            {
                -1,0,1
            },
            RequestedStatus = RequestedStatus,
            RequestedUserId = user.Data.Id,
            StepId = AppConstant.PreparePsychoMedState,
            PageIndex = pageIndex,
            PageSize = PageSize
        };
        var specification = new ExecutorProfileSpecification(parameter);
        var result = await _mediator.Send(new ListProfileWithSpecAsyncQuery(specification));
        var pagination = new Pagination<ProfileDTO>(
            pageIndex,
            PageSize,
            (await _mediator.Send(new CountProfileAsyncQuery(new ExecutorProfileSpecification(parameter,false)))),
            result.Data
        );
        return Ok(pagination);
    }
    //Отправка на подписание
    [AuthorizeByRole(AppConstant.ExecutorName)]
    [HttpGet]
    public async Task<ActionResult<Pagination<ProfileDTO>>> SendOffer([FromQuery]int pageIndex = 1,int PageSize = 50,[FromQuery(Name = "RequestedStatus")] List<int> RequestedStatus = null)
    {
        var user =    await  _mediator.Send(new GetUserByIdAsync(new UserSpecification(User.FindFirst(ClaimTypes.NameIdentifier)?.Value,null)));
        var parameter = new ExecutorProfileParameter()
        {
            AreaId = user.Data.AreaId,
            Status = new List<int>()
            {
                -1,0,1
            },
            RequestedStatus = RequestedStatus,
            RequestedUserId = user.Data.Id,
            StepId = AppConstant.OfferState,
            PageIndex = pageIndex,
            PageSize = PageSize
        };
        var specification = new ExecutorProfileSpecification(parameter);
        var result = await _mediator.Send(new ListProfileWithSpecAsyncQuery(specification));
        var pagination = new Pagination<ProfileDTO>(
            pageIndex,
            PageSize,
            (await _mediator.Send(new CountProfileAsyncQuery(new ExecutorProfileSpecification(parameter,false)))),
            result.Data
        );
        return Ok(pagination);
    }
    //Подписанные
    [AuthorizeByRole(AppConstant.ExecutorName)]
    [HttpGet]
    public async Task<ActionResult<Pagination<ProfileDTO>>> Signing([FromQuery]int pageIndex = 1,int PageSize = 50,[FromQuery(Name = "RequestedStatus")] List<int> RequestedStatus = null)
    {
        var user =    await  _mediator.Send(new GetUserByIdAsync(new UserSpecification(User.FindFirst(ClaimTypes.NameIdentifier)?.Value,null)));
        var parameter = new ExecutorProfileParameter()
        {
            AreaId = user.Data.AreaId,
            Status = new List<int>()
            {
                -1,0,1
            },
            RequestedUserId = user.Data.Id,
            RequestedStatus = RequestedStatus,
            StepId = AppConstant.SigningState,
            PageIndex = pageIndex,
            PageSize = PageSize
        };
        var specification = new ExecutorProfileSpecification(parameter);
        var result = await _mediator.Send(new ListProfileWithSpecAsyncQuery(specification));
        var pagination = new Pagination<ProfileDTO>(
            pageIndex,
            PageSize,
            (await _mediator.Send(new CountProfileAsyncQuery(new ExecutorProfileSpecification(parameter,false)))),
            result.Data
        );
        return Ok(pagination);
    }
    
    [AuthorizeByRole(AppConstant.ExecutorName)]
    [HttpGet]
    public async Task<ActionResult<Pagination<ProfileDTO>>> Rejected([FromQuery]int pageIndex = 1,int PageSize = 50)
    {
        var user =    await  _mediator.Send(new GetUserByIdAsync(new UserSpecification(User.FindFirst(ClaimTypes.NameIdentifier)?.Value,null)));
        List<long> Surveys = await _surveyExecutorRepository.GetExecutorsSurvey(user.Data.Id);
        var specification = new ExecutorProfileSpecification(Surveys,-1,PageSize,pageIndex);
        var result = await _mediator.Send(new ListProfileWithSpecAsyncQuery(specification));
        var pagination = new Pagination<ProfileDTO>(
            pageIndex,
            PageSize,
            (await _mediator.Send(new CountProfileAsyncQuery(new ExecutorProfileSpecification(Surveys,-1,PageSize,pageIndex,false)))),
            result.Data
        );
        return Ok(pagination);
    }
    
    [AuthorizeByRole(AppConstant.ExecutorName)]
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
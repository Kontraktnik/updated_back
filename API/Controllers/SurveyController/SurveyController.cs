using System.Drawing;
using System.Globalization;
using System.Net;
using System.Security.Claims;
using API.Helpers;
using Application.DTO.Common;
using Application.DTO.Survey;
using Application.Features.SurveyCQRS.Command.AddSurvey;
using Application.Features.SurveyCQRS.Command.DeleteSurvey;
using Application.Features.SurveyCQRS.Command.UpdateSurvey;
using Application.Features.SurveyCQRS.Query.GetSurveyWithSpecAsync;
using Application.Features.UserCQRS.Query.GetUserByIdAsync;
using Domain;
using Infrastracture.Contracts.Specifications.SurveySpecification;
using Infrastracture.Contracts.Specifications.UserSpecification;
using Infrastracture.Helpers;
using Ionic.Zip;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Xceed.Document.NET;
using Xceed.Words.NET;

namespace API.Controllers.SurveyController;

public class SurveyController : BaseApiController
{
    private readonly IMediator _mediator;
    private readonly AppConfig _config;

    public SurveyController(IMediator mediator, AppConfig config)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _config = config;
    }

    //Сохранить
    [AuthorizeByRole(AppConstant.UserRoleName)]
    [HttpPost]
    [ProducesResponseType(typeof(ResponseDTO<SurveyDTO>), (int)HttpStatusCode.Created)]
    public async Task<ActionResult<ResponseDTO<SurveyDTO>>> Save([FromBody] SurveyCUDTO model)
    {
        return await _mediator.Send(new AddSurveyCommand(model, User.FindFirst(ClaimTypes.NameIdentifier)?.Value));
    }
    //Обновить
    [AuthorizeByRole(AppConstant.UserRoleName)]
    [HttpPut]
    [ProducesResponseType(typeof(ResponseDTO<bool>), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<ResponseDTO<bool>>> Update([FromBody] SurveyMedDTO model, [FromQuery] long SurveyId)
    {
        var user = await _mediator.Send(new GetUserByIdAsync(new UserSpecification(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, null)));
        return await _mediator.Send(new UpdateSurveyCommand(user.Data, SurveyId, model));
    }

    [AuthorizeByRole(AppConstant.DirectorName, AppConstant.ExecutorName, AppConstant.KNBRoleName, AppConstant.UserRoleName, AppConstant.MEDRoleName)]
    [HttpGet]
    public async Task<ActionResult<ResponseDTO<SurveyDTO>>> GetById([FromQuery] long Id)
    {
        var user = await _mediator.Send(new GetUserByIdAsync(new UserSpecification(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, null)));
        SurveySpecification specification = new SurveySpecification(Id, user.Data);
        var result = await _mediator.Send(new GetSurveyWithSpecAsyncQuery(specification, user.Data));
        return StatusCode(result.StatusCode, result);
    }

    [HttpDelete]
    [AuthorizeByRole(AppConstant.UserRoleName)]
    public async Task<ActionResult<ResponseDTO<bool>>> Delete([FromQuery] long Id)
    {
        var user = await _mediator.Send(new GetUserByIdAsync(new UserSpecification(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, null)));
        var result = await _mediator.Send(new DeleteSurveyCommand(Id, user.Data));
        return StatusCode(result.StatusCode, result);
    }


    [AuthorizeByRole(AppConstant.DirectorName, AppConstant.ExecutorName, AppConstant.KNBRoleName)]
    [HttpPost]
    public async Task<ActionResult<ResponseDTO<String>>> GetSurveyInfo([FromQuery] long Id)
    {
        var user = await _mediator.Send(new GetUserByIdAsync(new UserSpecification(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, null)));
        SurveySpecification specification = new SurveySpecification(Id, user.Data);
        var result = await _mediator.Send(new GetSurveyWithSpecAsyncQuery(specification, user.Data));
        return await Report.getSurveyInfo(result.Data, user.Data, _config.FileStoragePath);

    }

}
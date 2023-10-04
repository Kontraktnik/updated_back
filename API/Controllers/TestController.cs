using API.Hubs;
using Application.Contracts.Persistence;
using Application.Contracts.Service;
using Application.DTO.Calculation.PositionDTO;
using Application.DTO.Calculation.RankSalaryDTO;
using Application.DTO.Common;
using Application.DTO.EmailDTO;
using Application.DTO.Profile;
using Application.DTO.Survey;
using Application.Features.SurveyCQRS.Query.GetSurveyForEmailWithSpecAsync;
using AutoMapper;
using Domain;
using Infrastracture.Contracts.Specifications.SurveySpecification;
using Infrastracture.Database;
using Infrastracture.Helpers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
namespace API.Controllers;

public class TestController : BaseApiController
{
    private IRankSalaryRepository _repository;
    private AppDbContext _context;
    private IHubContext<TestHub> _hub;
    private IEmailService _emailService;
    private IMediator _mediator;
    private IConfiguration _configuration;
    public TestController(IRankSalaryRepository repository,AppDbContext context,IHubContext<TestHub> hub,IEmailService emailService,IMediator mediator, IConfiguration configuration)
    {
        _repository = repository;
        _context = context;
        _hub = hub;
        _emailService = emailService;
        _mediator = mediator;
        _configuration = configuration;

    }
    [HttpGet]
    public async Task<ResponseDTO<RankSalaryRDTO>> GetByRankId([FromQuery] long RankId)
    {
        return await _repository.GetByRankId(RankId);
    }
    
    [HttpPost]
    public async Task<dynamic> TestPost()
    {
        var requestDto = new SendRequestDTO() { Status = 1, SignKey = "32", StepId = AppConstant.SendedState, SurveyId = 1 };
        await _hub.Clients.All.SendAsync("sendTestMessage", requestDto);
        return requestDto;
    }
    
    [HttpPost]
    public async Task Statics([FromQuery] long SurveyId)
    {
      //  await _emailService.SendNotificationEmail((await _mediator.Send(new GetSurveyForEmailWithSpecAsyncQuery(new SurveyMailSpecification(SurveyId)))).Data);

    }
}
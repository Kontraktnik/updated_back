using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Common;
using Application.DTO.Survey;
using Application.Helpers;
using Application.Resource;
using AutoMapper;
using Domain.Models.SurveyModels;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Features.SurveyExecutorCQRS.Command.AddSurveyExecutor;

public class AddSurveyExecutorCommandHandler : IRequestHandler<AddSurveyExecutorCommand,ResponseDTO<SurveyExecutorDTO>>
{
    public AddSurveyExecutorCommandHandler(IMapper mapper,  IUserRepository userRepository, ISurveyRepository surveyRepository,  ISurveyExecutorRepository surveyExecutorRepository,IStringLocalizer<Localize> localizer)
    {
        _mapper = mapper;
        _userRepository = userRepository;
        _surveyRepository = surveyRepository;
        _surveyExecutorRepository = surveyExecutorRepository;
        this.localizer = localizer;
    }

    private IStringLocalizer<Localize> localizer;
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;
    private readonly  ISurveyRepository _surveyRepository;
    private readonly ISurveyExecutorRepository _surveyExecutorRepository;
    public async Task<ResponseDTO<SurveyExecutorDTO>> Handle(AddSurveyExecutorCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var director = await _userRepository.GetByIdAsync(request.model.DirectorId);
            var executor = await _userRepository.GetByIdAsync(request.model.ExecutorId);
            var survey = await _surveyRepository.GetByIdAsync(request.model.SurveyId);
            var surveyExecutor = await _surveyExecutorRepository.GetBySurveyId(request.model.SurveyId);
            //ЕСЛИ ПАРА ДИРЕКТОР - ИСПОЛНИТЕЛЬ НЕ СОЗДАНА
            if (surveyExecutor == null)
            {
                //Если такого директора не существует
                if (director.Equals(null) )
                    {
                    return new ResponseDTO<SurveyExecutorDTO>
                        {
                            Success = false,
                            StatusCode = (int) HttpStatusCode.NotFound,
                            Message = $"{this.localizer["NotFound"]}"
                        };
                    }
                //Если такого исполнителя не существует
                if (executor.Equals(null) )
                {
                    return new ResponseDTO<SurveyExecutorDTO>
                    {
                        Success = false,
                        StatusCode = (int) HttpStatusCode.NotFound,
                        Message = $"{this.localizer["NotFound"]}"
                    };
                }
                
            //Если такой заявки не существует
            if (survey.Equals(null))
                {
                    return new ResponseDTO<SurveyExecutorDTO>
                    {
                        Success = false,
                        StatusCode = (int) HttpStatusCode.NotFound,
                        Message =  $"{this.localizer["NotFound"]} : {localizer["SurveyId"]}"
                    };
                }
            if (!director.Equals(null) && executor.AreaId == survey.BirthAreaId)
            {
                if (survey.CurrentStepId == 1 && survey.Status == 0)
                {
                    var newSurveyExecutor = await _surveyExecutorRepository.AddAsync(_mapper.Map<SurveyExecutor>(request.model));
                    return new ResponseDTO<SurveyExecutorDTO>
                    {
                        Success = true,
                        StatusCode = (int) HttpStatusCode.OK,
                        Data = _mapper.Map<SurveyExecutorDTO>(newSurveyExecutor)
                    };
                }
                else
                {
                    return new ResponseDTO<SurveyExecutorDTO>
                    {
                        Success = false,
                        StatusCode = (int) HttpStatusCode.BadRequest,
                        Message = $"{this.localizer["NotFound"]}"
                    };
                }
            }
            else
            {
                return new ResponseDTO<SurveyExecutorDTO>
                {
                    Success = false,
                    StatusCode = (int) HttpStatusCode.BadRequest,
                    Message = $"{this.localizer["Forbidden"]}"
                };
            }
            }
            return new ResponseDTO<SurveyExecutorDTO>
            {
                Success = true,
                StatusCode = (int) HttpStatusCode.BadRequest,
                Message = $"{this.localizer["Existed"]}",
                Data = _mapper.Map<SurveyExecutorDTO>(surveyExecutor)
            };
        }
        catch (Exception ex)
        {
            return new ResponseDTO<SurveyExecutorDTO>
            {
                Success = false,
                StatusCode = (int) HttpStatusCode.InternalServerError,
                Message = this.localizer["UnexpectedError"] +":" + ex.Message,
            };
        }
    }
}
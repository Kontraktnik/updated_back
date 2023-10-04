using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Common;
using Application.DTO.Survey;
using Application.Helpers;
using Application.Resource;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Features.SurveyCQRS.Query.GetSurveyWithSpecAsync;

public class GetSurveyWithSpecAsyncQueryHandler : IRequestHandler<GetSurveyWithSpecAsyncQuery,ResponseDTO<SurveyDTO>>
{
    private readonly ISurveyRepository _surveyRepository;
    private readonly ISurveyExecutorRepository _surveyExecutorRepository;
    private IMapper _mapper;
    private IStringLocalizer<Localize> localizer;
    public GetSurveyWithSpecAsyncQueryHandler(IMapper mapper, ISurveyRepository surveyRepository,ISurveyExecutorRepository surveyExecutorRepository, IStringLocalizer<Localize> localizer)
    {
        _surveyRepository = surveyRepository;
        _surveyExecutorRepository = surveyExecutorRepository;
        _mapper = mapper;
        this.localizer = localizer;
    }

    public async Task<ResponseDTO<SurveyDTO>> Handle(GetSurveyWithSpecAsyncQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var user = request.UserDto;
            var specification = request.specification;
            var survey = await _surveyRepository.GetEntityWithSpecAsync(specification);
            if (survey != null)
            {
                if (user.RoleId.Equals(ValidationHelpers.DirectorRoleId) ||
                    user.RoleId.Equals(ValidationHelpers.ExecutorRoleId))
                {
                    var surveyExecutor = await _surveyExecutorRepository.GetBySurveyId(survey.Id);
                    
                        if (user.RoleId.Equals(ValidationHelpers.DirectorRoleId))
                        {
                            if (surveyExecutor != null && surveyExecutor.DirectorId.Equals(user.Id))
                            {
                                return new ResponseDTO<SurveyDTO>
                                {
                                    Success = true,
                                    StatusCode = (int) HttpStatusCode.OK,
                                    Data = _mapper.Map<SurveyDTO>(survey)
                                };
                            }
                            if (survey.CurrentStepId == ValidationHelpers.SendedState )
                            {
                                return new ResponseDTO<SurveyDTO>
                                {
                                    Success = true,
                                    StatusCode = (int) HttpStatusCode.OK,
                                    Data = _mapper.Map<SurveyDTO>(survey)
                                };
                            }     
                        }
                        else if (user.RoleId.Equals(ValidationHelpers.ExecutorRoleId))
                        {
                            if (surveyExecutor != null && surveyExecutor.ExecutorId.Equals(user.Id))
                            {
                                return new ResponseDTO<SurveyDTO>
                                {
                                    Success = true,
                                    StatusCode = (int) HttpStatusCode.OK,
                                    Data = _mapper.Map<SurveyDTO>(survey)
                                };
                            } 
                        }
                        return new ResponseDTO<SurveyDTO>
                        {
                            Success = false,
                            StatusCode = (int) HttpStatusCode.Forbidden,
                            Message = this.localizer["Forbidden"]
                        };
                    
                }
                else if (user.RoleId.Equals(ValidationHelpers.KNBRoleId))
                {
                    //Оставляем лишь данные спец проверки
                    survey.Profiles = survey.Profiles.Where(p =>
                            p.StepId == ValidationHelpers.SpecialState)
                        .ToList();
                    
                    if (survey.BirthAreaId.Equals(user.AreaId))
                    {
                        return new ResponseDTO<SurveyDTO>
                        {
                            Success = true,
                            StatusCode = (int) HttpStatusCode.OK,
                            Data = _mapper.Map<SurveyDTO>(survey)
                        };
                    }
                }
                else if (user.RoleId.Equals(ValidationHelpers.MEDRoleId))
                {
                    //Оставляем лишь данные, мед и психологического тестирования
                    survey.Profiles = survey.Profiles.Where(p =>
                            p.StepId == ValidationHelpers.MedState || p.StepId == ValidationHelpers.PsychoMedState)
                        .ToList();
                    
                    if (survey.BirthAreaId.Equals(user.AreaId))
                    {
                        return new ResponseDTO<SurveyDTO>
                        {
                            Success = true,
                            StatusCode = (int) HttpStatusCode.OK,
                            Data = _mapper.Map<SurveyDTO>(survey)
                        };
                    }
                }
                else if (user.RoleId.Equals(ValidationHelpers.UserRoleId))
                {
                    if (survey.UserId.Equals(user.Id))
                    {
                        //Удаляем файлы спецпроверки, мед и психологического тестирования
                        foreach (var prof in survey.Profiles)
                        {
                            if (prof.StepId == ValidationHelpers.SpecialState ||
                                prof.StepId == ValidationHelpers.MedState ||
                                prof.StepId == ValidationHelpers.PsychoMedState)
                            {
                                prof.ProfileFiles = prof.ProfileFiles.Where(p => p.ProfileId != prof.Id).ToList();
                            }

                            if (prof.StepId == ValidationHelpers.MedState || prof.StepId == ValidationHelpers.PsychoMedState)
                            {
                                prof.Comment = null;
                            }
                        }
                        
                        
                        return new ResponseDTO<SurveyDTO>
                        {
                            Success = true,
                            StatusCode = (int) HttpStatusCode.OK,
                            Data = _mapper.Map<SurveyDTO>(survey)
                        };
                    }
                }
                return new ResponseDTO<SurveyDTO>
                    {
                        Success = false,
                        StatusCode = (int) HttpStatusCode.Forbidden,
                        Message = this.localizer["Forbidden"]
                    };
                
            }
            else
            {
                return new ResponseDTO<SurveyDTO>
                {
                    Success = false,
                    StatusCode = (int) HttpStatusCode.NotFound,
                    Message = this.localizer["NotFound"]
                };
            }
            
        }
        catch (Exception ex)
        {
            return new ResponseDTO<SurveyDTO>
            {
                Success = false,
                StatusCode = (int) HttpStatusCode.InternalServerError,
                Message = ex.Message
            };
        }
        
        
    }
}
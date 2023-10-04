using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Common;
using Application.Helpers;
using Application.Resource;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Features.SurveyCQRS.Command.UpdateSurvey;

public class UpdateSurveyCommandHandler : IRequestHandler<UpdateSurveyCommand,ResponseDTO<bool>>
{
    private ISurveyRepository _surveyRepository;
    private IStringLocalizer<Localize> _stringLocalizer;

    public UpdateSurveyCommandHandler(ISurveyRepository surveyRepository,IStringLocalizer<Localize> stringLocalizer)
    {
        _surveyRepository = surveyRepository;
        _stringLocalizer = stringLocalizer;
    }


    public async Task<ResponseDTO<bool>> Handle(UpdateSurveyCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = request.user;
            var model = request.model;
            //НАЙДЕМ ТЕКУЩУЮ ЗАЯВКУ
            var survey = await _surveyRepository.GetByIdAsync(request.SurveyId);
            if (survey != null)
            {
                //Проверяем уровень доступности
                if (survey.UserId == user.Id && survey.CurrentStepId == ValidationHelpers.PreparedMedState)
                {
                    survey.TuberUrl = model.TuberUrl;
                    survey.DermatologUrl = model.DermatologUrl;
                    survey.PsychoNeurologicalUrl = model.PsychoNeurologicalUrl;
                    survey.NarcologicalUrl = model.NarcologicalUrl;
                    await _surveyRepository.UpdateAsync(survey);
                    return new ResponseDTO<bool>()
                    {
                        Success = true,
                        StatusCode = (int) HttpStatusCode.OK,
                        Data = true,
                        Message = _stringLocalizer["Updated"]
                    };
                }
                else
                {
                    return new ResponseDTO<bool>()
                    {
                        Success = false,
                        StatusCode = (int) HttpStatusCode.BadRequest,
                        Data = false,
                        Message = _stringLocalizer["Forbidden"]
                    };
                }
            }
            else
            {
                return new ResponseDTO<bool>()
                {
                    Success = false,
                    StatusCode = (int) HttpStatusCode.BadRequest,
                    Data = false,
                    Message = _stringLocalizer["NotFound"]
                };
            }
        }
        catch (Exception ex)
        {
            return new ResponseDTO<bool>()
            {
                Success = false,
                StatusCode = (int) HttpStatusCode.InternalServerError,
                Data = false,
                Message = ex.ToString()
            };
        }
    }
}
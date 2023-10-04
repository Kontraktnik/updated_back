using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Common;
using Application.Resource;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Features.SurveyCQRS.Command.DeleteSurvey;

public class DeleteSurveyCommandHandler : IRequestHandler<DeleteSurveyCommand,ResponseDTO<bool>>
{
    public DeleteSurveyCommandHandler(ISurveyRepository surveyRepository, ISurveyDriverRepository surveyDriverRepository, ISurveyRelativeRepository surveyRelativeRepository, IStringLocalizer<Localize> localizer,ISurveyExecutorRepository surveyExecutorRepository)
    {
        _surveyRepository = surveyRepository;
        _surveyDriverRepository = surveyDriverRepository;
        _surveyRelativeRepository = surveyRelativeRepository;
        this.localizer = localizer;
        _surveyExecutorRepository = surveyExecutorRepository;
    }

    private ISurveyRepository _surveyRepository;
    private ISurveyExecutorRepository _surveyExecutorRepository;
    private ISurveyDriverRepository _surveyDriverRepository;
    private ISurveyRelativeRepository _surveyRelativeRepository;
    private IStringLocalizer<Localize> localizer { get; set; }


    public async Task<ResponseDTO<bool>> Handle(DeleteSurveyCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var survey = await _surveyRepository.GetByIdAsync(request.Id);
            if (survey != null)
            {
                if (survey.UserId == request.UserDto.Id)
                {
                    var surveyExecutor = await _surveyExecutorRepository.GetBySurveyId(request.Id);
                    if (surveyExecutor == null && survey.CurrentStepId == null)
                    {
                        await _surveyRepository.DeleteAsync(survey);
                        return new ResponseDTO<bool>
                        {
                            Success = true,
                            StatusCode = (int) HttpStatusCode.OK,
                            Message = localizer["Deleted"],
                            Data = true
                        };
                    }
                    else
                    {
                        return new ResponseDTO<bool>
                        {
                            Success = false,
                            StatusCode = (int) HttpStatusCode.Forbidden,
                            Message = localizer["Forbidden"],
                            Data = false
                        };
                    }
                }
                else
                {
                    return new ResponseDTO<bool>
                    {
                        Success = false,
                        StatusCode = (int) HttpStatusCode.Forbidden,
                        Message = localizer["Forbidden"],
                        Data = false
                    };
                }
            }
            else
            {
                return new ResponseDTO<bool>
                {
                    Success = false,
                    StatusCode = (int) HttpStatusCode.NotFound,
                    Message = localizer["NotFound"],
                    Data = false
                };
            }
        }
        catch (Exception ex)
        {
            return new ResponseDTO<bool>
            {
                Success = false,
                StatusCode = (int) HttpStatusCode.InternalServerError,
                Message = ex.ToString(),
                Data = false
            };
        }
    }
}
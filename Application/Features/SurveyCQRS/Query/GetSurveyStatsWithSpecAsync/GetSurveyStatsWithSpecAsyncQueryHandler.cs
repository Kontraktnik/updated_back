using System.Net;
using Application.Contracts.Persistence;
using Application.DTO.Common;
using Application.DTO.Surveys.SurveyStatistics;
using Application.Resource;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;

namespace Application.Features.SurveyCQRS.Query.GetSurveyStatsWithSpecAsync;

public class GetSurveyStatsWithSpecAsyncQueryHandler : IRequestHandler<GetSurveyStatsWithSpecAsyncQuery,ResponseDTO<SurveyStatisticsAllDTO>>
{
    public GetSurveyStatsWithSpecAsyncQueryHandler(ISurveyRepository surveyRepository, ISurveyExecutorRepository surveyExecutorRepository,IMapper mapper, IStringLocalizer<Localize> localizer)
    {
        _surveyRepository = surveyRepository;
        _surveyExecutorRepository = surveyExecutorRepository;
        _mapper = mapper;
        this.localizer = localizer;
    }

    private ISurveyRepository _surveyRepository;
    private ISurveyExecutorRepository _surveyExecutorRepository;
    private IMapper _mapper;
    private IStringLocalizer<Localize> localizer;


    public async Task<ResponseDTO<SurveyStatisticsAllDTO>> Handle(GetSurveyStatsWithSpecAsyncQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var executors = await _surveyExecutorRepository.GetExecutorsSurvey(request.ExecutorId??0);
            var specification = request.Specification;
            var statsAll = await _surveyRepository.getCountStatistics(specification, executors);
            var statsByStep = await _surveyRepository.getCountByStepStatistics(specification, executors);
            return new ResponseDTO<SurveyStatisticsAllDTO>
            {
                StatusCode = (int) HttpStatusCode.OK,
                Success = true,
                Data = new SurveyStatisticsAllDTO()
                {
                    Count = await _surveyRepository.CountAsync(specification),
                    All = _mapper.Map<List<SurveyStatisticsDTO>>(statsAll),
                    ByStepId = _mapper.Map<List<SurveyStatisticsDTO>>(statsByStep)
                }
                
            };

        }
        catch (Exception ex)
        {
            return new ResponseDTO<SurveyStatisticsAllDTO>
            {
                StatusCode = (int) HttpStatusCode.InternalServerError,
                Success = false,
                Message = ex.ToString(),
                
            };
        }
    }
}
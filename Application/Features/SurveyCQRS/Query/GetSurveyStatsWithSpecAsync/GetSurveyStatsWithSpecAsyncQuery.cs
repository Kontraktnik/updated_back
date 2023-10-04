using Application.Contracts.Specification;
using Application.DTO.Common;
using Application.DTO.Surveys.SurveyStatistics;
using Domain.Models.SurveyModels;
using MediatR;

namespace Application.Features.SurveyCQRS.Query.GetSurveyStatsWithSpecAsync;

public class GetSurveyStatsWithSpecAsyncQuery : IRequest<ResponseDTO<SurveyStatisticsAllDTO>>
{
    public GetSurveyStatsWithSpecAsyncQuery(ISpecification<Survey> specification, long? executorId)
    {
        Specification = specification;
        ExecutorId = executorId;
    }

    public ISpecification<Survey> Specification { get; set; }
    public long? ExecutorId { get; set; }
    
    



}
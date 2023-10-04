using Application.Contracts.Specification;
using Application.DTO.Common;
using Application.DTO.Survey;
using Domain.Models.SurveyModels;

namespace Application.Contracts.Persistence;

public interface ISurveyRepository : IGenericRepository<Survey>
{
    public Task<List<long>> GetSurveyByUserId(long UserId);

    public Task<Survey> GetSurveyByVacancyIdAndUserId(long UserId, long VacancyId);
    
    public Task<List<SurveyStatistics>> getCountByStepStatistics(ISpecification<Survey> specification,List<long>? SurveyId);
    public Task<List<SurveyStatistics>> getCountStatistics(ISpecification<Survey> specification,List<long>? SurveyId);
}
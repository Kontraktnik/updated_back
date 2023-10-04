using Domain.Models.SurveyModels;

namespace Application.Contracts.Persistence;

public interface ISurveyExecutorRepository : IGenericRepository<SurveyExecutor>
{
    public Task<SurveyExecutor> GetBySurveyId(long SurveyId);

    public Task<List<long>> GetExecutorsSurvey(long ExecutorId);
    public Task<List<long>> GetDirectorSurvey(long DirectorId);


}
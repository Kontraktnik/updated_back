using Application.Contracts.Persistence;
using Domain.Models.SurveyModels;
using Infrastracture.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastracture.Contracts.Repositories;

public class SurveyExecutorRepository : GenericRepository<SurveyExecutor>,ISurveyExecutorRepository
{
    private readonly AppDbContext _context;
    public SurveyExecutorRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<SurveyExecutor> GetBySurveyId(long SurveyId)
    {
        return await _context.SurveyExecutors.AsNoTracking().FirstOrDefaultAsync(p => p.SurveyId.Equals(SurveyId));
    }

    public async Task<List<long>> GetExecutorsSurvey(long ExecutorId)
    {
        return await _context.SurveyExecutors.Where(p => p.ExecutorId.Equals(ExecutorId)).Select(p => p.SurveyId).ToListAsync();
    }

    public async Task<List<long>> GetDirectorSurvey(long DirectorId)
    {
        return await _context.SurveyExecutors.Where(p => p.DirectorId.Equals(DirectorId)).Select(p => p.SurveyId).ToListAsync();
    }
}
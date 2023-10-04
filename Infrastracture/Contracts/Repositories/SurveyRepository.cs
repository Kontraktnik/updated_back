using System.Net;
using Application.Contracts.Persistence;
using Application.Contracts.Specification;
using Application.DTO.Common;
using Application.DTO.Survey;
using Domain.Models.StepModels;
using Domain.Models.SurveyModels;
using Infrastracture.Database;
using Infrastracture.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Infrastracture.Contracts.Repositories;

public class SurveyRepository : GenericRepository<Survey>,ISurveyRepository
{
    private readonly AppDbContext _context;
    public SurveyRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<List<long>> GetSurveyByUserId(long UserId)
    {
        return await _context.Surveys.Where(p => p.UserId == UserId).Select(p => p.Id).ToListAsync();
    }

    public async Task<Survey> GetSurveyByVacancyIdAndUserId(long UserId, long VacancyId)
    {
        return await _context.Surveys.Where(p => p.UserId.Equals(UserId) && p.VacancyId.Equals(VacancyId) && (p.CurrentStep.Equals(null) ? p.Status.Equals(0) : p.Status.Equals(-1) ) ).FirstOrDefaultAsync();
    }

    public async Task<List<SurveyStatistics>> getCountByStepStatistics(ISpecification<Survey> specification,List<long>? SurveyId)
    {
        return await ApplySpecification(specification)
            .Where(p=> (SurveyId == null || SurveyId.Count == 0 || SurveyId.Contains(p.Id)))
            .GroupBy(p =>
                new {
                    p.CurrentStepId,
                    p.Status
                })
            .Select(g => new SurveyStatistics
            {
                StepId = g.Key.CurrentStepId,
                Status = g.Key.Status,
                Count = g.Count()
            })
            .ToListAsync();
        
    }

    public async Task<List<SurveyStatistics>> getCountStatistics(ISpecification<Survey> specification,List<long>? SurveyId)
    {
        return await ApplySpecification(specification)
            .Where(p=> (SurveyId == null || SurveyId.Count == 0 || SurveyId.Contains(p.Id)))
            .GroupBy(p =>
                new {
                    p.Status
                })
            .Select(g => new SurveyStatistics
            {
                Status = g.Key.Status,
                Count = g.Count()
            })
            .ToListAsync();
    }
}
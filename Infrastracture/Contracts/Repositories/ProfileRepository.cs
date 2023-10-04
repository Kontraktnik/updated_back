using Application.Contracts.Persistence;
using Application.Contracts.Specification;
using AutoMapper;
using Domain.Models.SurveyModels;
using Infrastracture.Database;
using Microsoft.EntityFrameworkCore;
using Profile = Domain.Models.ProfileModels.Profile;

namespace Infrastracture.Contracts.Repositories;

public class ProfileRepository : GenericRepository<Profile>,IProfileRepository
{
    private readonly AppDbContext _context;
    public ProfileRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Profile> GetBySurveyAndStep(long SurveyId, long StepId)
    {
        return await _context.Profiles.AsNoTracking().FirstOrDefaultAsync(p => p.SurveyId.Equals(SurveyId) && p.StepId.Equals(StepId));
    }
    
}
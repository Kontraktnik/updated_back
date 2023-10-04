using Application.Contracts.Specification;
using AutoMapper;
using Domain.Models.SurveyModels;
using Profile = Domain.Models.ProfileModels.Profile;

namespace Application.Contracts.Persistence;

public interface IProfileRepository : IGenericRepository<Profile>
{
    public Task<Profile> GetBySurveyAndStep(long SurveyId, long StepId);
    
    

}
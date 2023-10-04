using Application.Contracts.Persistence;
using Domain.Models.SurveyModels;
using Infrastracture.Database;

namespace Infrastracture.Contracts.Repositories;

public class SurveyRelativeRepository : GenericRepository<SurveyRelative>,ISurveyRelativeRepository
{
    public SurveyRelativeRepository(AppDbContext context) : base(context)
    {
    }
}
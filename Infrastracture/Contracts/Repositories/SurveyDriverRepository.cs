using Application.Contracts.Persistence;
using Application.Contracts.Specification;
using Domain.Models.SurveyModels;
using Infrastracture.Database;

namespace Infrastracture.Contracts.Repositories;

public class SurveyDriverRepository : GenericRepository<SurveyDriver>,ISurveyDriverRepository
{
    public SurveyDriverRepository(AppDbContext context) : base(context)
    {
    }

    
}
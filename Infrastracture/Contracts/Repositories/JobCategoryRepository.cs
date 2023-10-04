using Application.Contracts.Persistence;
using Domain.Models.CalculationModels;
using Infrastracture.Database;

namespace Infrastracture.Contracts.Repositories;

public class JobCategoryRepository : GenericRepository<JobCategory>,IJobCategoryRepository
{
    public JobCategoryRepository(AppDbContext context) : base(context)
    {
    }
}
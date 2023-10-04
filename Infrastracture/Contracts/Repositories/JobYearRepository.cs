using Application.Contracts.Persistence;
using Domain.Models.CalculationModels;
using Infrastracture.Database;

namespace Infrastracture.Contracts.Repositories;

public class JobYearRepository : GenericRepository<JobYear>,IJobYearRepository
{
    public JobYearRepository(AppDbContext context) : base(context)
    {
    }
}
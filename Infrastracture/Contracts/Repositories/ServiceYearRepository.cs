using Application.Contracts.Persistence;
using Domain.Models.CalculationModels;
using Infrastracture.Database;

namespace Infrastracture.Contracts.Repositories;

public class ServiceYearRepository : GenericRepository<ServiceYear>,IServiceYearRepository
{
    public ServiceYearRepository(AppDbContext context) : base(context)
    {
    }
}
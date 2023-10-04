using Application.Contracts.Persistence;
using Domain.Models.CalculationModels;
using Infrastracture.Database;

namespace Infrastracture.Contracts.Repositories;

public class PositionRepository : GenericRepository<Position>,IPositionRepository
{
    public PositionRepository(AppDbContext context) : base(context)
    {
    }
}
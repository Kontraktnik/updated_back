using Application.Contracts.Persistence;
using Domain.Models.SystemModels;
using Infrastracture.Database;

namespace Infrastracture.Contracts.Repositories;

public class ArmyRegionRepository : GenericRepository<ArmyRegion>,IArmyRegionRepository
{
    public ArmyRegionRepository(AppDbContext context) : base(context)
    {
    }
}
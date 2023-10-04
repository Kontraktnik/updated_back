using Application.Contracts.Persistence;
using Domain.Models.SystemModels;
using Infrastracture.Database;

namespace Infrastracture.Contracts.Repositories;

public class ArmyRankRepository : GenericRepository<ArmyRank>,IArmyRankRepository
{
    public ArmyRankRepository(AppDbContext context) : base(context)
    {
        
    }
}
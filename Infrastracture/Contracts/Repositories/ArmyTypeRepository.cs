using Application.Contracts.Persistence;
using Domain.Models.SystemModels;
using Infrastracture.Database;

namespace Infrastracture.Contracts.Repositories;

public class ArmyTypeRepository : GenericRepository<ArmyType>,IArmyTypeRepository
{
    public ArmyTypeRepository(AppDbContext context) : base(context)
    {
    }
}
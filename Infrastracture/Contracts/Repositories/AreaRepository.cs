using Application.Contracts.Persistence;
using Domain.Models.SystemModels;
using Infrastracture.Database;

namespace Infrastracture.Contracts.Repositories;

public class AreaRepository : GenericRepository<Area>,IAreaRepository
{
    public AreaRepository(AppDbContext context) : base(context)
    {
        
    }
}
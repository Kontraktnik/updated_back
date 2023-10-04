using Application.Contracts.Persistence;
using Domain.Models.SystemModels;
using Infrastracture.Database;

namespace Infrastracture.Contracts.Repositories;

public class RelativeRepository : GenericRepository<Relative>,IRelativeRepository
{
    public RelativeRepository(AppDbContext context) : base(context)
    {
        
    }
}
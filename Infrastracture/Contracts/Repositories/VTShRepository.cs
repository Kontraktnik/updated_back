using Application.Contracts.Persistence;
using Domain.Models.SystemModels;
using Infrastracture.Database;

namespace Infrastracture.Contracts.Repositories;

public class VTShRepository : GenericRepository<VTSh>,IVTShRepository
{
    public VTShRepository(AppDbContext context) : base(context)
    {
    }
}
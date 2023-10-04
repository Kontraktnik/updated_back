using Application.Contracts.Persistence;
using Domain.Models.ProfileModels;
using Infrastracture.Database;

namespace Infrastracture.Contracts.Repositories;

public class ProfileFileRepository : GenericRepository<ProfileFile>,IProfileFileRepository
{
    public ProfileFileRepository(AppDbContext context) : base(context)
    {
        
    }
}
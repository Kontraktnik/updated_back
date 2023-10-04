using Application.Contracts.Persistence;
using Domain.Models.UserModels;
using Infrastracture.Database;

namespace Infrastracture.Contracts.Repositories;

public class RoleRepository : GenericRepository<Role>,IRoleRepository
{
    public RoleRepository(AppDbContext context) : base(context)
    {
    }
}
using Application.Contracts.Persistence;
using Domain.Models.SystemModels;
using Infrastracture.Database;

namespace Infrastracture.Contracts.Repositories;

public class ArmyDepartmentRepository : GenericRepository<ArmyDepartment>,IArmyDepartmentRepository
{
    public ArmyDepartmentRepository(AppDbContext context) : base(context)
    {
        
    }
}
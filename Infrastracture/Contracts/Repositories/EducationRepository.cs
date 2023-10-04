using Application.Contracts.Persistence;
using Domain.Models.SystemModels;
using Infrastracture.Database;

namespace Infrastracture.Contracts.Repositories;

public class EducationRepository : GenericRepository<Education>,IEducationRepository
{
    public EducationRepository(AppDbContext context) : base(context)
    {
    }
}
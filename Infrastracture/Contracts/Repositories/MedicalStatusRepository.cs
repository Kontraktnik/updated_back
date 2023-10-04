using Application.Contracts.Persistence;
using Domain.Models.SystemModels;
using Infrastracture.Database;

namespace Infrastracture.Contracts.Repositories;

public class MedicalStatusRepository : GenericRepository<MedicalStatus>,IMedicalStatusRepository
{
    public MedicalStatusRepository(AppDbContext context) : base(context)
    {
    }
}
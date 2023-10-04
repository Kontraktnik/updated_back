using Application.Contracts.Persistence;
using Domain.Models.VacancyModel;
using Infrastracture.Database;

namespace Infrastracture.Contracts.Repositories;

public class VacancyRepository : GenericRepository<Vacancy>,IVacancyRepository
{
    public VacancyRepository(AppDbContext context) : base(context)
    {
    }
}
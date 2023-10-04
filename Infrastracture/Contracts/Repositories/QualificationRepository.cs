using Application.Contracts.Persistence;
using Domain.Models.CalculationModels;
using Infrastracture.Database;

namespace Infrastracture.Contracts.Repositories;

public class QualificationRepository : GenericRepository<Qualification>,IQualificationRepository
{
    public QualificationRepository(AppDbContext context) : base(context)
    {
    }
}
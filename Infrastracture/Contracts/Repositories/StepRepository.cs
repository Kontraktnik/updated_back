using Application.Contracts.Persistence;
using Domain.Models.StepModels;
using Infrastracture.Database;

namespace Infrastracture.Contracts.Repositories;

public class StepRepository : GenericRepository<Step>,IStepRepository
{
    public StepRepository(AppDbContext context) : base(context)
    {
    }
}
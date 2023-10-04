using Application.Contracts.Persistence;
using Domain.Models.StepModels;
using Infrastracture.Database;

namespace Infrastracture.Contracts.Repositories;

public class StepGroupRepository : GenericRepository<StepGroup>,IStepGroupRepository
{
    public StepGroupRepository(AppDbContext context) : base(context)
    {
        
    }
}
using Application.Contracts.Persistence;
using Application.DTO.Step;
using Domain.Models.StepModels;
using Infrastracture.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastracture.Contracts.Repositories;

public class StepOrderRepository : GenericRepository<StepOrder>,IStepOrderRepository
{
    private readonly AppDbContext _context;
    public StepOrderRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public  async Task<StepOrder> getByStepId(long StepId)
    {
        return  await _context.StepOrders.AsNoTracking().FirstOrDefaultAsync(p => p.StepId == StepId);
    }
}
using Application.DTO.Step;
using Domain.Models.StepModels;

namespace Application.Contracts.Persistence;

public interface IStepOrderRepository : IGenericRepository<StepOrder>
{
    public Task<StepOrder> getByStepId(long StepId);
}
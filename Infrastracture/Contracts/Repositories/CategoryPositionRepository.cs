using Application.Contracts.Persistence;
using Domain.Models.CalculationModels;
using Infrastracture.Database;

namespace Infrastracture.Contracts.Repositories;

public class CategoryPositionRepository : GenericRepository<CategoryPosition>,ICategoryPositionRepository
{
    public CategoryPositionRepository(AppDbContext context) : base(context)
    {
    }
}
using Application.Contracts.Persistence;
using Domain.Models.CalculationModels;
using Infrastracture.Database;

namespace Infrastracture.Contracts.Repositories;

public class SecretLevelRepository : GenericRepository<SecretLevel>,ISecretLevelRepository
{
    public SecretLevelRepository(AppDbContext context) : base(context)
    {
    }
}
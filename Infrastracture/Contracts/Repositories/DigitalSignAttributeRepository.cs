using Application.Contracts.Persistence;
using Domain.Models.DigitalSignModels;
using Infrastracture.Database;

namespace Infrastracture.Contracts.Repositories;

public class DigitalSignAttributeRepository : GenericRepository<DigitalSignAttribute>, IDigitalSignAttributeRepository
{
    public DigitalSignAttributeRepository(AppDbContext context) : base(context)
    {
    }
}

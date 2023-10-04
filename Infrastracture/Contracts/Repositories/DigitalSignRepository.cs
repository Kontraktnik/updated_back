using Application.Contracts.Persistence;
using Domain.Models.DigitalSignModels;
using Infrastracture.Database;

namespace Infrastracture.Contracts.Repositories;

public class DigitalSignRepository : GenericRepository<DigitalSign>, IDigitalSignRepository
{
    public DigitalSignRepository(AppDbContext context) : base(context)
    {
    }
}

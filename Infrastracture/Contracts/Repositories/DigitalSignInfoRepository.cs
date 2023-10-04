using Application.Contracts.Persistence;
using Domain.Models.DigitalSignModels;
using Infrastracture.Database;

namespace Infrastracture.Contracts.Repositories;

public class DigitalSignInfoRepository : GenericRepository<DigitalSignInfo>, IDigitalSignInfoRepository
{
    public DigitalSignInfoRepository(AppDbContext context) : base(context)
    {
    }
}

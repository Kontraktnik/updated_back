using Application.Contracts.Persistence;
using Domain.Models.DigitalSignModels;
using Infrastracture.Database;

namespace Infrastracture.Contracts.Repositories;

public class DigitalSignBinaryRepository : GenericRepository<DigitalSignBinary>, IDigitalSignBinaryRepository
{
    public DigitalSignBinaryRepository(AppDbContext context) : base(context)
    {
    }
}

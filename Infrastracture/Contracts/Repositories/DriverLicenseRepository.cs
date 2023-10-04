using Application.Contracts.Persistence;
using Domain.Models.SystemModels;
using Infrastracture.Database;

namespace Infrastracture.Contracts.Repositories;

public class DriverLicenseRepository : GenericRepository<DriverLicense>,IDriverLicenseRepository
{
    public DriverLicenseRepository(AppDbContext context) : base(context)
    {
    }
}
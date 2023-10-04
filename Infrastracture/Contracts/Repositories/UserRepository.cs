using Application.Contracts.Persistence;
using Domain.Models.UserModels;
using Infrastracture.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastracture.Contracts.Repositories;

public class UserRepository : GenericRepository<User>,IUserRepository
{
    private AppDbContext _context;
    public UserRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }


    public async Task<User> getUserByEmail(string Email)
    {
        return await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email.Equals(Email));
    }

    public async Task<User> getUserByIIN(string IIN)
    {
        return await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.IIN.Equals(IIN));
    }

    public async Task<User> getUserByPhone(string Phone)
    {
        return  await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Phone.Equals(Phone));
    }
}
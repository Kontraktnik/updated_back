using Domain.Models.UserModels;

namespace Application.Contracts.Persistence;

public interface IUserRepository : IGenericRepository<User>
{
    public Task<User> getUserByEmail(string Email);
    public Task<User> getUserByIIN(string IIN);
    public Task<User> getUserByPhone(string Phone);

    
}
using Infrastructure.Models;

namespace Infrastructure.Interfaces.Repositories;

public interface IUserRepository
{
    public Task<DbUser> AddUser(string email, string hashedPassword, string role);
    public Task<DbUser?> GetUserByEmail(string email);
    public Task<DbUser?> GetUserById(int id);
}

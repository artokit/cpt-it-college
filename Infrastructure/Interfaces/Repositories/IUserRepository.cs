using Infrastructure.Models;

namespace Infrastructure.Interfaces.Repositories;

public interface IUserRepository
{
    public Task<DbUser> AddUser(string email, string hashedPassword, string role);
}

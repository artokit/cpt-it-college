using Infrastructure.Dapper;
using Infrastructure.Dapper.Interfaces;
using Infrastructure.Interfaces.Repositories;
using Infrastructure.Models;

namespace Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private IDapperContext dapperContext;

    public UserRepository(IDapperContext dapperContext)
    {
        this.dapperContext = dapperContext;
    }
    
    public async Task<DbUser> AddUser(string email, string hashedPassword, string role)
    {
        var query = new QueryObject("INSERT INTO users(email, hashed_password, role) VALUES(@email, @hashed_password, @role) returning id as \"Id\", email as \"Email\", hashed_password as \"HashedPassword\", role as \"Role\"", new {email, hashed_password=hashedPassword, role});
        return await dapperContext.CommandWithResponse<DbUser>(query);
    }

    public async Task<DbUser?> GetUserById(int id)
    {
        var query = new QueryObject("SELECT id as \"Id\", email as \"Email\", hashed_password as \"HashedPassword\", role as \"Role\" FROM USERS where id = @id", new { id });
        return await dapperContext.FirstOrDefault<DbUser>(query);
    }
    
    public async Task<DbUser?> GetUserByEmail(string email)
    {
        var query = new QueryObject("SELECT id as \"Id\", email as \"Email\", hashed_password as \"HashedPassword\", role as \"Role\" FROM USERS where email = @email", new { email });
        return await dapperContext.FirstOrDefault<DbUser>(query);
    }
}

using Infrastructure.Dapper;
using Infrastructure.Dapper.Interfaces;
using Infrastructure.Interfaces.Repositories;
using Infrastructure.Models;

namespace Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private IDapperContext _dapperContext;

    public UserRepository(IDapperContext dapperContext)
    {
        _dapperContext = dapperContext;
    }
    
    public async Task<DbUser> AddUser(string email, string hashedPassword, string role)
    {
        var query = new QueryObject("INSERT INTO users(email, hashed_password, role) VALUES(@email, @hashed_password, @role) returning *", new {email, hashed_password=hashedPassword, role});
        return await _dapperContext.CommandWithResponse<DbUser>(query);
    }
}

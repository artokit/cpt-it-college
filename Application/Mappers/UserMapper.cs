using Domain;
using Infrastructure.Models;

namespace Application.Mappers;

public static class UserMapper
{
    public static User? MapToDomain(this DbUser? dbUser)
    {
        return dbUser is null ? null : new User { Email = dbUser.Email, Role = dbUser.Role, Id = dbUser.Id };
    }
    
    public static DbUser MapToDb(this User user)
    {
        return new DbUser { Email = user.Email, Role = user.Role, Id = user.Id };
    }
}

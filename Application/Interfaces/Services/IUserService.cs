using Application.DTO;
using Infrastructure.Models;

namespace Application.Interfaces.Services;

public interface IUserService
{
    public Task<DbUser> AddUser(RegisterUserDto registerUserDto);
}

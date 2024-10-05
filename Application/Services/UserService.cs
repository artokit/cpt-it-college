using Application.DTO;
using Application.Interfaces.Services;
using Infrastructure.Interfaces.Repositories;
using Infrastructure.Models;

namespace Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository userRepository;
    
    public UserService(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }
    
    public async Task<DbUser> AddUser(RegisterUserDto registerUserDto)
    {
        var res = await userRepository.AddUser(registerUserDto.Email, registerUserDto.Password, registerUserDto.Role);
        Console.WriteLine(res);
        return res;
    }
}

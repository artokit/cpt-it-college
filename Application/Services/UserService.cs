using Application.DTO;
using Application.Interfaces.Services;
using Infrastructure.Interfaces.Repositories;

namespace Application.Services;

public class UserService : IUserService
{
    private IUserRepository _userRepository;
    public UserService(IUserRepository usersRepository)
    {
        _userRepository = usersRepository;
    }
    
    public async Task<GetMeResponseDto> GetMe(int userId)
    {
        var res = await _userRepository.GetUserById(userId);
        return new GetMeResponseDto { Id = res.Id, Role = res.Role, Email = res.Email };
    }
}

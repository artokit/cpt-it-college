using Application.DTO;

namespace Application.Interfaces.Services;

public interface IUserService
{
    public Task<GetMeResponseDto> GetMe(int userId);
}

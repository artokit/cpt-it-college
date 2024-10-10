using Application.DTO;
using Infrastructure.Models;

namespace Application.Interfaces.Services;

public interface IAuthService
{
    public Task<SuccessLoginResponseDto> Register(RegisterUserRequestDto registerUserRequestDto);
    public Task<SuccessLoginResponseDto> Login(LoginUserRequestDto loginUserRequestDto);
    public Task<SuccessLoginResponseDto> RefreshToken(string refreshToken);
}

using Application.DTO;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/auth/")]
public class AuthController : Controller
{
    private IAuthService authService;
    
    public AuthController(IAuthService authService)
    {
        this.authService = authService;
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterUserRequestDto registerUserRequestDto)
    {
        return Ok(await authService.Register(registerUserRequestDto));
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginUserRequestDto loginUserRequestDto)
    {
        return Ok(await authService.Login(loginUserRequestDto));
    }
    
    [HttpPost("refresh-token")]
    public async Task<IActionResult> Refresh(string refreshToken)
    {
        return Ok(await authService.RefreshToken(refreshToken));
    }
}

using Application.DTO;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/auth/")]
public class AuthController : Controller
{
    private IUserService userService;
    
    public AuthController(IUserService userService)
    {
        this.userService = userService;
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterUserDto registerUserDto)
    {
        return Ok(await userService.AddUser(registerUserDto));
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login()
    {
        return Ok("");
    }
}

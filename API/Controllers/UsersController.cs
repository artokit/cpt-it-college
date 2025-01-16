using Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/users")]
public class UsersController(IUserService userService) : BaseController
{
    [Authorize(Roles = "Reader,Author")]
    [HttpGet("me")]    
    public async Task<IActionResult> GetMe()
    {
        return Ok(await userService.GetMe(UserId));
    }
}

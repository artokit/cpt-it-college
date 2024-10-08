using Application.Interfaces.Services;
using Common.Enums;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("api/posts")]
public class PostController : BaseController
{
    private IPostService postService;

    public PostController(IPostService postService)
    {
        this.postService = postService;
    }
    
    [HttpGet("")]    
    public async Task<IActionResult> GetPosts()
    {
        return Role == Roles.Reader ? Ok(await postService.GetPostsForReader()) : Ok(await postService.GetPostsForAuthor(UserId));
    }
}

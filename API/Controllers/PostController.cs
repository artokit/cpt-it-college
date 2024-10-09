using Application.DTO;
using Application.Interfaces.Services;
using Common.Enums;
using Microsoft.AspNetCore.Authorization;
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
    
    [Authorize(Roles = "Reader,Author")]
    [HttpGet("")]    
    public async Task<IActionResult> GetPosts()
    {
        return Ok(Role == Roles.Reader
            ? await postService.GetPostsForReader()
            : await postService.GetPostsForAuthor(UserId));
    }
    
    [Authorize(Roles = "Author")]
    [HttpPost("")]    
    public async Task<IActionResult> AddPosts(AddNewPostRequestDto addNewPostRequestDto)
    {
        await postService.AddPost(addNewPostRequestDto, UserId);
        return Ok();
    }
}

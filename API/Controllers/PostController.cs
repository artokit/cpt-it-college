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
    public async Task<IActionResult> AddPost(AddNewPostRequestDto addNewPostRequestDto)
    {
        var res = await postService.AddPost(UserId, addNewPostRequestDto);
        return Ok(res);
    }
    
    [Authorize(Roles = "Author")]
    [HttpPut("{postId:int}")]    
    public async Task<IActionResult> EditPost(int postId, EditPostRequestDto editPostRequestDto)
    {
        await postService.UpdatePost(UserId, postId, editPostRequestDto);
        return Ok();
    }

    [Authorize(Roles = "Author")]
    [HttpPatch("{postId:int}/status")]
    public async Task<IActionResult> PublicPost(int postId, ChangePostStatusDto changePostStatusDto)
    {
        await postService.PublishPost(UserId, postId, changePostStatusDto);
        return Ok();
    }

    [Authorize(Roles = "Author")]
    [HttpPost("/api/posts/{postId:int}/images")]
    public async Task<IActionResult> AddImageToPost(int postId, IFormFile image)
    {
        await using var stream = image.OpenReadStream();
        await postService.AddImageToPost(postId, image.FileName, stream);
        return Ok();
    }
}

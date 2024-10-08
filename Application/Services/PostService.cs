using Application.DTO;
using Application.Interfaces.Services;
using Domain;
using Infrastructure.Interfaces.Repositories;
using Infrastructure.Models;

namespace Application.Services;

public class PostService : IPostService
{
    private IPostRepository postRepository;

    public PostService(IPostRepository postRepository)
    {
        this.postRepository = postRepository;
    }
    
    public async Task<PostsListResponse> GetPostsForReader()
    {
        return new PostsListResponse {Result = await postRepository.GetAllPosts()};
    }

    public async Task<PostsListResponse> GetPostsForAuthor(int userId)
    {
        return new PostsListResponse {Result = await postRepository.GetAllPostsByUserId(userId)};
    }
}

using Application.DTO;
using Application.Interfaces.Services;
using Application.Mappers;
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
        return new PostsListResponse {Result = (await postRepository.GetAllPublishedPosts()).MapToDomain()};
    }

    public async Task<PostsListResponse> GetPostsForAuthor(int userId)
    {
        return new PostsListResponse {Result = (await postRepository.GetAllPostsByUserId(userId)).MapToDomain()};
    }

    public async Task<Post> AddPost(AddNewPostRequestDto addNewPostRequestDto, int authorId)
    {
        return (await postRepository.AddPost(
            new DbPost
            {
                Content = addNewPostRequestDto.Content, Title = addNewPostRequestDto.Title, AuthorId = authorId
            })).MapToDomain();
    }
}

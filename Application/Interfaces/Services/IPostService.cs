using Application.DTO;
using Domain;

namespace Application.Interfaces.Services;

public interface IPostService
{
    public Task<PostsListResponse> GetPostsForReader();
    public Task<PostsListResponse> GetPostsForAuthor(int userId);
    public Task<Post> AddPost(AddNewPostRequestDto addNewPostRequestDto, int authorId);
}

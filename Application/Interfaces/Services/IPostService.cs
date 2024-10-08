using Application.DTO;

namespace Application.Interfaces.Services;

public interface IPostService
{
    public Task<PostsListResponse> GetPostsForReader();
    public Task<PostsListResponse> GetPostsForAuthor(int userId);
}

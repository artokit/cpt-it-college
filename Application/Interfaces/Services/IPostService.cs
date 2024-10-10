using Application.DTO;
using Domain;

namespace Application.Interfaces.Services;

public interface IPostService
{
    public Task<PostsListResponse> GetPostsForReader();
    public Task<PostsListResponse> GetPostsForAuthor(int userId);
    public Task<Post> AddPost(int authorId, AddNewPostRequestDto addNewPostRequestDto);
    public Task UpdatePost(int userId, int postId, EditPostRequestDto editPostRequestDto);
    public Task PublishPost(int userId, int postId, ChangePostStatusDto changePostStatusDto);
}

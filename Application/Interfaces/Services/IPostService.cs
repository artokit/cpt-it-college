using Application.DTO;
using Domain;

namespace Application.Interfaces.Services;

public interface IPostService
{
    public Task<List<PostResponseDto>> GetPostsForReader();
    public Task<List<PostResponseDto>> GetPostsForAuthor(int userId);
    public Task<PostResponseDto> AddPost(int authorId, AddNewPostRequestDto addNewPostRequestDto);
    public Task UpdatePost(int userId, int postId, EditPostRequestDto editPostRequestDto);
    public Task PublishPost(int userId, int postId, ChangePostStatusDto changePostStatusDto);
    public Task<PostResponseDto> AddImageToPost(int postId, string objectName, Stream image);
    public Task DeleteImageFromPost(int postId, int imageId, int userId);
}

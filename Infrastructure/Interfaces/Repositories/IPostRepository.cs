using Infrastructure.Models;

namespace Infrastructure.Interfaces.Repositories;

public interface IPostRepository
{
    public Task<List<DbPost>> GetAllPublishedPosts();
    public Task<List<DbPost>> GetAllPostsByUserId(int userId);
    public Task<DbPost> GetPostById(int id);
    public Task<DbPost> AddPost(DbPost dbPost);
    public Task<DbPost?> UpdatePost(DbPost dbPost);
    public Task<DbPost?> DeleteImageFromPost(int postId, int imageId);
    public Task<DbPost?> PublishPost(int id);
    public Task<DbPost?> AddImageToPost(int postId, DbImage dbImage);
}

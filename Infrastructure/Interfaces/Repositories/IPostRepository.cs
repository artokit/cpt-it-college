using Infrastructure.Models;

namespace Infrastructure.Interfaces.Repositories;

public interface IPostRepository
{
    public Task<List<DbPost>> GetAllPosts();
    public Task<List<DbPost>> GetAllPostsByUserId(int userId);
}

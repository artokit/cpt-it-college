using Domain;
using Infrastructure.Dapper;
using Infrastructure.Dapper.Interfaces;
using Infrastructure.Interfaces.Repositories;
using Infrastructure.Models;

namespace Infrastructure.Repositories;

public class PostRepository : IPostRepository
{
    private IDapperContext dapperContext;
    
    public PostRepository(IDapperContext dapperContext)
    {
        this.dapperContext = dapperContext;
    }
    
    public async Task<List<DbPost>> GetAllPosts()
    {
        var q = new QueryObject(
            @"SELECT p.id , p.author_id as ""AuthorId"", p.title as ""Title"", p.Content as ""Content"", p.created_at as ""CreateedAt"", p.updated_at as ""UpdatedAt"", p.Status,
                       i.id, i.image_uuid as ""ImageUuid"", i.created_at as ""CreatedAt""
                FROM posts p
                LEFT JOIN images i ON p.Id = i.post_id;");
        var c = await dapperContext.QueryWithJoin<DbPost, DbImage, Post>(q, (q, q1) =>
        {
            Console.WriteLine(q.Id);
            Console.WriteLine(q1.Id);
            return new Post();
        }, "id");
        var query = new QueryObject("SELECT id as \"Id\", author_id as \"AuthorId\", title as \"Title\", content as \"Content\", created_at as \"CreatedAt\", updated_at as \"UpdatedAt\", status as \"Status\" FROM POSTS WHERE status='published'");
        return await dapperContext.ListOrEmpty<DbPost>(query);
    }

    public async Task<List<DbPost>> GetAllPostsByUserId(int userId)
    {
        var query = new QueryObject("SELECT id as \"Id\", author_id as \"AuthorId\", title as \"Title\", content as \"Content\", created_at as \"CreatedAt\", updated_at as \"UpdatedAt\", status as \"Status\" FROM POSTS WHERE author_id=@userId", new {userId});
        return await dapperContext.ListOrEmpty<DbPost>(query);
    }
}

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
    
    public async Task<List<DbPost>> GetAllPublishedPosts()
    {
        var queryObject = new QueryObject(
            @"SELECT p.id, p.author_id as ""AuthorId"", p.title as ""Title"", p.Content as ""Content"", p.created_at as ""CreatedAt"", p.updated_at as ""UpdatedAt"", p.Status,
                       i.id, i.post_id as ""PostId"", i.image_name as ""ImageName"", i.created_at as ""CreatedAt""
                FROM posts p
                LEFT JOIN images i ON p.Id = i.post_id WHERE status = 'published'");
        var res = await GetPostWithImages(queryObject);
        return res;
    }

    public async Task<List<DbPost>> GetAllPostsByUserId(int userId)
    {
        var queryObject = new QueryObject(
            @"SELECT p.id, p.author_id as ""AuthorId"", p.title as ""Title"", p.Content as ""Content"", p.created_at as ""CreatedAt"", p.updated_at as ""UpdatedAt"", p.Status,
                       i.id, i.post_id as ""PostId"", i.image_name as ""ImageName"", i.created_at as ""CreatedAt""
                FROM posts p
                LEFT JOIN images i ON p.Id = i.post_id where author_id=@author_id", new {author_id=userId});
        var res = await GetPostWithImages(queryObject);
        return res;
    }

    public async Task<DbPost?> GetPostById(int id)
    {
        var queryObject = new QueryObject(
            @"SELECT p.id, p.author_id as ""AuthorId"", p.title as ""Title"", p.Content as ""Content"", p.created_at as ""CreatedAt"", p.updated_at as ""UpdatedAt"", p.Status,
                       i.id, i.post_id as ""PostId"", i.image_name as ""ImageName"", i.created_at as ""CreatedAt""
                FROM posts p
                LEFT JOIN images i ON p.Id = i.post_id where p.id = @post_id", new {post_id=id});
        var res = await GetPostWithImages(queryObject);
        return res.Count != 0 ? res[0] : null;
    }

    public async Task<DbPost> AddPost(DbPost dbPost)
    {
        var queryObject = new QueryObject(
            @"INSERT INTO posts(author_id, title, content, created_at, updated_at, status) VALUES(@author_id, @title, @content, now(), now(), 'draft')
            RETURNING id, author_id as ""AuthorId"", title as ""Title"", Content as ""Content"", created_at as ""CreatedAt"", updated_at as ""UpdatedAt"", Status",
            new { author_id = dbPost.AuthorId, title=dbPost.Title, content=dbPost.Content });
        var res = await dapperContext.CommandWithResponse<DbPost>(queryObject);
        return res;
    }

    public async Task UpdatePost(DbPost dbPost)
    {
        var queryObject = new QueryObject("UPDATE posts SET title = @title, content = @content, updated_at = now() where id = @post_id",
            new { title = dbPost.Title, content = dbPost.Content, post_id = dbPost.Id });
        await dapperContext.Command(queryObject);
    }

    public async Task DeleteImage(int imageId)
    {
        var queryObject = new QueryObject("DELETE FROM IMAGES where id = @imageId", new {imageId});
        await dapperContext.Command(queryObject);
    }

    public async Task PublishPost(int id)
    {
        var queryObject = new QueryObject("UPDATE posts SET status = 'published' where id = @id", new {id});
        await dapperContext.Command(queryObject);
    }

    public async Task<DbImage> AddImageToPost(int postId, string imageName)
    {
        var queryObject =
            new QueryObject(
                "INSERT INTO IMAGES(post_id, image_name, created_at) VALUES(@postId, @imageName, now()) RETURNING id as \"Id\", post_id as \"PostId\", image_name as \"ImageName\", created_at as \"CreatedAt\"",
                new { postId, imageName });
        return await dapperContext.CommandWithResponse<DbImage>(queryObject);
    }

    private async Task<List<DbPost>> GetPostWithImages(IQueryObject queryObject)
    {
        var dictionary = new Dictionary<int, DbPost>();
        var res = await dapperContext.QueryWithJoin<DbPost, DbImage?, DbPost>(queryObject, (post, image) =>
        {
            DbPost p;
            if (!dictionary.TryGetValue(post.Id, out p))
            {
                p = post;
                p.Images = new List<DbImage>();
                dictionary.Add(p.Id, p);
            }

            if (p.Id == image?.PostId)
            {
                p.Images?.Add(image);
            }

            return p;
        }, "id");

        return res.Distinct().ToList();
    }
}

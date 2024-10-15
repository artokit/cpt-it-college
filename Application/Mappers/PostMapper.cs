using Domain;
using Infrastructure.Models;

namespace Application.Mappers;

public static class PostMapper
{
    public static Post MapToDomain(this DbPost dbPost)
    {
        var p = new Post
        {
            Id = dbPost.Id,
            AuthorId = dbPost.AuthorId,
            Title = dbPost.Title,
            Content = dbPost.Content,
            CreatedAt = dbPost.CreatedAt,
            UpdatedAt = dbPost.UpdatedAt,
            Status = dbPost.Status,
            Images = (dbPost.Images != null && dbPost.Images.Count != 0) ? dbPost.Images.MapToDomain() : []
        };
        return p;
    }

    public static List<Post> MapToDomain(this List<DbPost> dbPosts)
    {
        return dbPosts.Select(i => i.MapToDomain()).ToList();
    }
}

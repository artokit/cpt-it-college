using Application.DTO;
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
            IdempotencyKey = dbPost.IdempotencyKey,
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

    public static PostResponseDto MapToDto(this Post post)
    {
        return new PostResponseDto
        {
            Id = post.Id,
            AuthorId = post.AuthorId,
            Title = post.Title,
            Content = post.Content,
            CreatedAt = post.CreatedAt,
            UpdatedAt = post.UpdatedAt,
            Status = post.Status.ToString().ToLower(),
            Images = post.Images.MapToDto()
        };
    }
    
    public static List<PostResponseDto> MapToDto(this List<Post> posts)
    {
        return posts.Select(post => post.MapToDto()).ToList();
    }
}

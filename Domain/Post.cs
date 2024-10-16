using Domain.Enums;

namespace Domain;

public class Post
{
    public int Id { get; set; }
    public int AuthorId { get; set; }
    public string IdempotencyKey { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public PostStatus Status { get; set; }
    public List<Image> Images { get; set; }
}

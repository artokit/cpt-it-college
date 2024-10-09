using Domain.Enums;

namespace Infrastructure.Models;

public class DbPost
{
    public int Id { get; set; }
    public int AuthorId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public PostStatus Status { get; set; }
    public List<DbImage>? Images { get; set; }
}

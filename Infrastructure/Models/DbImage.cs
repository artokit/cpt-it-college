namespace Infrastructure.Models;

public class DbImage
{
    public int Id { get; set; }
    public int PostId { get; set; }
    public string ImageName { get; set; }
    public DateTime CreatedAt { get; set; }
}

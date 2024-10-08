namespace Infrastructure.Models;

public class DbImage
{
    public int Id { get; set; }
    public int PostId { get; set; }
    public string ImageUuid { get; set; }
    public DateTime CreateAt { get; set; }
}

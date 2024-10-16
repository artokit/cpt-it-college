namespace Domain;

public class Image
{
    public int Id { get; set; }
    public int PostId { get; set; }
    public string ImageName { get; set; }
    public DateTime CreatedAt { get; set; }
}

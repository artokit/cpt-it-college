using System.ComponentModel;

namespace Application.DTO;

public class AddNewPostRequestDto
{
    public string Title { get; set; }
    public string Content { get; set; }
    public string IdempotencyKey { get; set; }
}

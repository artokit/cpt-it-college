using Infrastructure.Models;

namespace Application.DTO;

public class PostsListResponse
{
    public List<DbPost> Result { get; set; }
}

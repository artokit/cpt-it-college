using Common.Enums;

namespace Application.DTO;

public class GetMeResponseDto
{
    public int Id { get; set; }
    public Roles Role { get; set; }
    public string Email { get; set; }
}

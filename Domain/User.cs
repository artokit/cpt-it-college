using Common;
using Common.Enums;

namespace Domain;

public class User
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string HashedPassword { get; set; }
    public Roles Role { get; set; }
}

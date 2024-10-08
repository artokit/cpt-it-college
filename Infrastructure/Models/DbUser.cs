using Common;
using Common.Enums;

namespace Infrastructure.Models;

public class DbUser
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string HashedPassword { get; set; }
    public Roles Role { get; set; }
}

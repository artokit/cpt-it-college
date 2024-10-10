using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Application.DTO;

public class RegisterUserRequestDto
{
    [EmailAddress(ErrorMessage = "Некорректный email адрес")]
    public string Email { get; set; }
    
    public string Password { get; set; }
    
    [DefaultValue("reader")]
    public string Role { get; set; }
}

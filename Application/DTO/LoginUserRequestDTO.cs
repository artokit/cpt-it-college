using System.ComponentModel.DataAnnotations;

namespace Application.DTO;

public class LoginUserRequestDto
{    
    [EmailAddress(ErrorMessage = "Некорректный email адрес")]
    public string Email { get; set; }
    public string Password { get; set; }
}

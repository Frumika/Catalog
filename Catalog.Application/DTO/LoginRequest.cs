using System.ComponentModel.DataAnnotations;

namespace Catalog.Application.DTO;

public class LoginRequest
{
    [Required(ErrorMessage = "Login is required")]
    public string Login { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; } = string.Empty;
}
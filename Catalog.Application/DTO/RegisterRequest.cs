using System.ComponentModel.DataAnnotations;

namespace Catalog.Application.DTO;

public class RegisterRequest
{
    [Required(ErrorMessage = "Email is required")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Login is required")]
    public string Login { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; } = string.Empty;

    public override string ToString()
    {
        return $"Email: {Email}, Login: {Login}, Password: {Password}";
    }
}
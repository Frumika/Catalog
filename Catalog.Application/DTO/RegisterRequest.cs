using System.ComponentModel.DataAnnotations;

namespace Catalog.Application.DTO;

public class RegisterRequest
{
    public string Email { get; set; } = string.Empty;
    public string Login { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
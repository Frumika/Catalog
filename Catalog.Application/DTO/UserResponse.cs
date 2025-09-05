using Catalog.Application.Enums;

namespace Catalog.Application.DTO;

public class UserResponse
{
    public bool IsSuccess { get; set; }
    public string? Message { get; set; } = string.Empty;
    public UserResponseCode? Code { get; set; }
}
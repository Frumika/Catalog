using Catalog.Application.Enums;

namespace Catalog.Application.DTO;

public class UserResponse
{
    public bool IsSuccess { get; set; }
    public string? Message { get; set; } = string.Empty;
    public IdentityResultCode? Code { get; set; }

    public static UserResponse Success(string? message = null)
    {
        return new()
        {
            IsSuccess = true,
            Message = message,
            Code = IdentityResultCode.Success
        };
    }

    public static UserResponse Fail(IdentityResultCode code, string? message = null)
    {
        return new()
        {
            IsSuccess = false,
            Message = message,
            Code = code
        };
    }
}
using Catalog.Application.Enums;

namespace Catalog.Application.DTO;

public class IdentityResponse
{
    public bool IsSuccess { get; set; }
    public string? Message { get; set; } = string.Empty;
    public IdentityResultCode? Code { get; set; }

    public static IdentityResponse Success(string? message = null)
    {
        return new()
        {
            IsSuccess = true,
            Message = message,
            Code = IdentityResultCode.Success
        };
    }

    public static IdentityResponse Fail(IdentityResultCode code, string? message = null)
    {
        return new()
        {
            IsSuccess = false,
            Message = message,
            Code = code
        };
    }
}
using Backend.Application.Enums;

namespace Backend.Application.DTO.Responses;

public class IdentityResponse : BaseResponse<IdentityStatus>
{
       public static IdentityResponse Success(string? message = null)
    {
        return new()
        {
            IsSuccess = true,
            Message = message,
            Code = IdentityStatus.Success
        };
    }

    public static IdentityResponse Fail(IdentityStatus code, string? message = null)
    {
        return new()
        {
            IsSuccess = false,
            Message = message,
            Code = code
        };
    }
}
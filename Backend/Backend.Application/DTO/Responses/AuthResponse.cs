using Backend.Application.StatusCodes;

namespace Backend.Application.DTO.Responses;

public class AuthResponse : BaseResponse<AuthStatusCode, AuthResponse>
{
    public new static AuthResponse Success<TData>(TData? data = null, string? message = null) where TData : class
    {
        return CreateSuccess(AuthStatusCode.Success, data, message);
    }

    public new static AuthResponse Success(string? message = null)
    {
        return CreateSuccess(AuthStatusCode.Success, message);
    }
}
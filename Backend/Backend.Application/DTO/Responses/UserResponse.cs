using Backend.Application.StatusCodes;

namespace Backend.Application.DTO.Responses;

public class UserResponse : BaseResponse<UserStatusCode, UserResponse>
{
    public new static UserResponse Success<TData>(TData? data = null, string? message = null) where TData : class
    {
        return CreateSuccess(UserStatusCode.Success, data, message);
    }

    public new static UserResponse Success(string? message = null)
    {
        return CreateSuccess(UserStatusCode.Success, message);
    }
}
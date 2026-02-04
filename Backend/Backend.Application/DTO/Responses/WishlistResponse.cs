using Backend.Application.StatusCodes;


namespace Backend.Application.DTO.Responses;

public class WishlistResponse : BaseResponse<WishlistStatusCode, WishlistResponse>
{
    public new static WishlistResponse Success<TData>(TData? data = null, string? message = null) where TData : class
    {
        return CreateSuccess(WishlistStatusCode.Success, data, message);
    }

    public new static WishlistResponse Success(string? message = null)
    {
        return CreateSuccess(WishlistStatusCode.Success, message);
    }
}
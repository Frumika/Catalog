using Backend.Application.StatusCodes;


namespace Backend.Application.DTO.Responses;

public class CartResponse : BaseResponse<CartStatusCode, CartResponse>
{
    public new static CartResponse Success<TData>(TData? data = null, string? message = null) where TData : class
    {
        return CreateSuccess(CartStatusCode.Success, data, message);
    }

    public new static CartResponse Success(string? message = null)
    {
        return CreateSuccess(CartStatusCode.Success, message);
    }
}
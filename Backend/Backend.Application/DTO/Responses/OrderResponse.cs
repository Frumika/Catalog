using Backend.Application.StatusCodes;


namespace Backend.Application.DTO.Responses;

public class OrderResponse : BaseResponse<OrderStatusCode, OrderResponse>
{
    public new static OrderResponse Success<TData>(TData? data = null, string? message = null) where TData : class
    {
        return CreateSuccess(OrderStatusCode.Success, data, message);
    }

    public new static OrderResponse Success(string? message = null)
    {
        return CreateSuccess(OrderStatusCode.Success, message);
    }
}
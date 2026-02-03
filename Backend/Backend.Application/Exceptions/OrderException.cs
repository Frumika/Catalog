using Backend.Application.StatusCodes;


namespace Backend.Application.Exceptions;

public class OrderException : BaseServiceException<OrderStatusCode>
{
    public OrderException(OrderStatusCode statusCode, string message) : base(statusCode, message)
    {
    }
}
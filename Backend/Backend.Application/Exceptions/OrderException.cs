using Backend.Application.StatusCodes;


namespace Backend.Application.Exceptions;

public class OrderException : Exception
{
    public OrderStatusCode StatusCode;

    public OrderException(OrderStatusCode statusCode, string message) : base(message)
    {
        StatusCode = statusCode;
    }
}
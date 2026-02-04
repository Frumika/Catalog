namespace Backend.Application.StatusCodes;

public enum OrderStatusCode
{
    Success,
    UserNotFound,
    CartNotFound,
    OrderNotFound,
    InvalidOrderStatus,
    IncorrectQuantity,
    BadRequest,
    UnknownError
}
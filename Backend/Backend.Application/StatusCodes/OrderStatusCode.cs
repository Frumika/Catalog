namespace Backend.Application.StatusCodes;

public enum OrderStatusCode
{
    Success,
    UserSessionNotFound,
    CartNotFound,
    OrderNotFound,
    InvalidOrderStatus,
    IncorrectQuantity,
    BadRequest,
    UnknownError
}
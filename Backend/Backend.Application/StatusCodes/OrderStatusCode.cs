namespace Backend.Application.StatusCodes;

public enum OrderStatusCode
{
    Success,
    BadRequest,
    UserSessionNotFound,
    CartNotFound,
    ProductNotFound,
    OrderNotFound,
    InvalidOrderStatus,
    IncorrectQuantity,
    UnknownError
}
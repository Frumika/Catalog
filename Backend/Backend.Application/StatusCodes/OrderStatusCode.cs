namespace Backend.Application.StatusCodes;

public enum OrderStatusCode
{
    Success,
    BadRequest,
    UserSessionNotFound,
    CartStateNotFound,
    ProductNotFound,
    IncorrectQuantity,
    UnknownError
}
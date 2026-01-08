namespace Backend.Application.StatusCodes;

public enum OrderStatusCode
{
    Success,
    BadRequest,
    UserSessionNotFound,
    CartStateNotFound,
    ProductNotFound,
    OrderStateNotFound,
    OrderStateNotCreated,
    OrderStateNotUpdated,
    IncorrectQuantity,
    UnknownError
}
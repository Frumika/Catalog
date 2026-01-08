namespace Backend.Application.StatusCodes;

public enum OrderStatusCode
{
    Success,
    BadRequest,
    UserSessionNotFound,
    CartStateNotFound,
    ProductNotFound,
    CartStateNotCreated,
    CartStateNotUpdated,
    IncorrectQuantity,
    UnknownError
}
namespace Backend.Application.StatusCodes;

public enum CartStatusCode
{
    Success,
    CartStateNotFound,
    CartStateNotCreated,
    CartStateNotUpdated,
    UserSessionNotFound,
    ProductNotFound,
    BadRequest,
    UnknownError
}
namespace Backend.Application.StatusCodes;

public enum CartStatusCode
{
    Success,
    CartStateNotFound,
    CartStateNotCreated,
    CartStateNotUpdated,
    CartStateNotDeleted,
    UserSessionNotFound,
    ProductNotFound,
    BadRequest,
    UnknownError
}
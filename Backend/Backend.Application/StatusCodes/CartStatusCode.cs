namespace Backend.Application.StatusCodes;

public enum CartStatusCode
{
    Success,
    CartStateNotFound,
    UserSessionNotFound,
    ProductNotFound,
    BadRequest,
    UnknownError
}
namespace Backend.Application.StatusCodes;

public enum CartStatusCode
{
    Success, 
    CartNotFound, 
    UserNotFound,
    ProductNotFound,
    BadRequest,
    UnknownError
}
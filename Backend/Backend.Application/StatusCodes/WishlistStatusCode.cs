namespace Backend.Application.StatusCodes;

public enum WishlistStatusCode
{
    Success, 
    WishlistNotFound, 
    UserNotFound,
    ProductNotFound,
    BadRequest,
    UnknownError
}
namespace Backend.Application.StatusCodes;

public enum ReviewStatusCode
{
    Success,
    UserNotFound,
    ProductNotFound,
    ReviewNotFound,
    ReviewAlreadyExist,
    BadRequest,
    UnknownError
}
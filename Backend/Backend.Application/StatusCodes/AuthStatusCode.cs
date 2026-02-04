namespace Backend.Application.StatusCodes;

public enum AuthStatusCode
{
    Success,
    InvalidPassword,
    UserAlreadyExists,
    UserNotFound,
    BadRequest,
    UnknownError
}
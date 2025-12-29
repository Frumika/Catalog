namespace Backend.Application.StatusCodes;

public enum AuthStatusCode
{
    Success,
    InvalidLogin,
    InvalidPassword,
    UserAlreadyExists,
    UserNotFound,
    SessionNotFound,
    BadRequest,
    UnknownError
}
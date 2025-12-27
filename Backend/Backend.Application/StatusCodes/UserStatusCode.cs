namespace Backend.Application.StatusCodes;

public enum UserStatusCode
{
    Success,
    InvalidLogin,
    InvalidPassword,
    IncorrectData,
    UserAlreadyExists,
    UserNotFound,
    UnknownError
}
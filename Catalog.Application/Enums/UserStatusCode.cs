namespace Catalog.Application.Enums;

public enum UserStatusCode
{
    Logged,
    Registered,
    InvalidCredentials,
    UserAlreadyExists,
    UserNotFound,
    UnknownError
}
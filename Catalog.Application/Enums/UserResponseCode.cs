namespace Catalog.Application.Enums;

public enum UserResponseCode
{
    Logged,
    Registered,
    InvalidCredentials,
    UserAlreadyExists,
    UserNotFound,
    UnknownError
}
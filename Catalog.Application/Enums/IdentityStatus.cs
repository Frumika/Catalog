namespace Catalog.Application.Enums;

public enum IdentityStatus
{
    Success,
    InvalidLogin,
    InvalidPassword,
    InvalidEmail,
    UserAlreadyExists,
    UserNotFound,
    UnknownError
}
namespace Catalog.Application.Enums;

public enum IdentityResultCode
{
    Success,
    InvalidLogin,
    InvalidPassword,
    InvalidEmail,
    UserAlreadyExists,
    UserNotFound,
    UnknownError
}
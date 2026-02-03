using Backend.Application.StatusCodes;


namespace Backend.Application.Exceptions;

public class AuthException : BaseServiceException<AuthStatusCode>
{
    public AuthException(AuthStatusCode statusCode, string message) : base(statusCode, message)
    {
    }
}
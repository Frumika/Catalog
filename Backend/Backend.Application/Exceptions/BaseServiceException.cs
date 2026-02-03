namespace Backend.Application.Exceptions;

public class BaseServiceException<TCode> : Exception where TCode : Enum
{
    public readonly TCode StatusCode;

    protected BaseServiceException(TCode statusCode, string message) : base(message)
    {
        StatusCode = statusCode;
    }
}
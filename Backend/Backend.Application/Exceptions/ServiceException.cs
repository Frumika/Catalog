using Backend.Application.Errors;


namespace Backend.Application.Exceptions;

public class ServiceException : Exception
{
    public readonly Error Error;
    
    public ServiceException(Error error, string message) : base(message)
    {
        Error = error;
    }
}
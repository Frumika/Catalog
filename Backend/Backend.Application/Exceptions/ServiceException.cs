using Backend.Application.Statuses;

namespace Backend.Application.Exceptions;

public class ServiceException : Exception
{
    public readonly Status Error;
    
    public ServiceException(Status error, string message) : base(message)
    {
        Error = error;
    }
}
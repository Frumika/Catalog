using Backend.Application.Common.Statuses;

namespace Backend.Application.Common;

public class ServiceException : Exception
{
    public readonly Status Error;

    public ServiceException(Status error, string message) : base(message)
    {
        Error = error;
    }
}
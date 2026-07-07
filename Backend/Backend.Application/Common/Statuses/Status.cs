namespace Backend.Application.Common.Statuses;

public abstract record Status
{
    public abstract string Code { get; }
}
namespace Backend.Application.Statuses;

public abstract record Status
{
    public abstract string Code { get; }
}
namespace Backend.Application.Errors;

public abstract record Error
{
    public abstract string Code { get; }
}
namespace Backend.Application.Errors;

public abstract record Unauthorized : Error;

public record InvalidPassword : Unauthorized
{
    public override string ToString() => nameof(InvalidPassword);
}
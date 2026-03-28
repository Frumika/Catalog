namespace Backend.Application.Errors;

public record UnknownError : Error
{
    public override string ToString() => nameof(UnknownError);
}
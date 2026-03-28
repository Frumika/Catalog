namespace Backend.Application.Errors;

public record UnknownError : Error
{
    public override string Code => "unknown_error";
}
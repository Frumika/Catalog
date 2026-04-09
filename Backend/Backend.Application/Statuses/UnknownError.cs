namespace Backend.Application.Statuses;

public record UnknownError : Status
{
    public override string Code => "unknown_error";
}
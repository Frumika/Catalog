namespace Backend.Application.Statuses;

public abstract record Unauthorized : Status;

public record InvalidPassword : Unauthorized
{
    public override string Code => "invalid_password";
}
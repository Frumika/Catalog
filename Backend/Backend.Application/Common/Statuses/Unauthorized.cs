namespace Backend.Application.Common.Statuses;

public abstract record Unauthorized : Status;

public record InvalidPassword : Unauthorized
{
    public override string Code => "invalid_password";
}

public record TokenRevoked : Unauthorized
{
    public override string Code => "token_revoked";
}

public record TokenExpired : Unauthorized
{
    public override string Code => "token_expired";
}
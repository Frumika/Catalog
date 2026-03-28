namespace Backend.Application.Errors;

public abstract record Conflict : Error;

public record UserAlreadyExists : Conflict
{
    public override string ToString() => nameof(UserAlreadyExists);
}

public record ReviewAlreadyExists : Conflict
{
    public override string ToString() => nameof(ReviewAlreadyExists);
}

public record InvalidOrderStatus : Conflict
{
    public override string ToString() => nameof(InvalidOrderStatus);
}
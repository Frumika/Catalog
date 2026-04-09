namespace Backend.Application.Statuses;

public abstract record Conflict : Status;

public record UserAlreadyExists : Conflict
{
    public override string Code => "user_already_exists";
}

public record ReviewAlreadyExists : Conflict
{
    public override string Code => "review_already_exists";
}

public record InvalidOrderStatus : Conflict
{
    public override string Code => "invalid_order_status";
}
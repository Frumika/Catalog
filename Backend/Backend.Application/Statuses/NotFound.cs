namespace Backend.Application.Statuses;

public abstract record NotFound : Status;

public record UserNotFound : NotFound
{
    public override string Code => "user_not_found";
}

public record UserSessionNotFound : NotFound
{
    public override string Code => "user_session_not_found";
}

public record CartNotFound : NotFound
{
    public override string Code => "cart_not_found";
}

public record OrderNotFound : NotFound
{
    public override string Code => "order_not_found";
}

public record ProductNotFound : NotFound
{
    public override string Code => "product_not_found";
}

public record ReviewNotFound : NotFound
{
    public override string Code => "review_not_found";
}
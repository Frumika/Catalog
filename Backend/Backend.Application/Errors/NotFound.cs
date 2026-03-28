namespace Backend.Application.Errors;

public abstract record NotFound : Error;

public record UserNotFound : NotFound
{
    public override string ToString() => nameof(UserNotFound);
}

public record UserSessionNotFound : NotFound
{
    public override string ToString() => nameof(UserSessionNotFound);
}

public record CartNotFound : NotFound
{
    public override string ToString() => nameof(CartNotFound);
}

public record OrderNotFound : NotFound
{
    public override string ToString() => nameof(OrderNotFound);
}

public record ProductNotFound : NotFound
{
    public override string ToString() => nameof(ProductNotFound);
}

public record ReviewNotFound : NotFound
{
    public override string ToString() => nameof(ReviewNotFound);
}
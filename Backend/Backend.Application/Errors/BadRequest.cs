namespace Backend.Application.Errors;

public record BadRequest : Error
{
    public override string ToString() => nameof(BadRequest);
}

public record IncorrectQuantity : BadRequest
{
    public override string ToString() => nameof(IncorrectQuantity);
}

public record IncorrectCategory : BadRequest
{
    public override string ToString() => nameof(IncorrectCategory);
}
namespace Backend.Application.Errors;

public record BadRequest : Error
{
    public override string Code => "bad_request";
}

public record IncorrectQuantity : BadRequest
{
    public override string Code => "incorrect_quantity";
}

public record IncorrectCategory : BadRequest
{
    public override string Code => "incorrect_category";
}
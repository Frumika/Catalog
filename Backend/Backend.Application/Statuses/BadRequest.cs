namespace Backend.Application.Statuses;

public record BadRequest : Status
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
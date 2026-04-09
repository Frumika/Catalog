namespace Backend.Application.Statuses;

public record Success : Status
{
    public override string Code => "success";
};
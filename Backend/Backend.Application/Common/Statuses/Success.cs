namespace Backend.Application.Common.Statuses;

public record Success : Status
{
    public override string Code => "success";
};
using System.Text.Json.Serialization;
using Backend.Application.Statuses;


namespace Backend.Application.Responses;

public class Response
{
    public string Code => Status.Code;
    public string? Message { get; init; }
    public object? Data { get; init; }
    [JsonIgnore] public Status Status { get; init; } = null!;

    private Response()
    {
    }

    public static Response Success<TData>(TData data, string? message = null) where TData : class
    {
        return new Response
        {
            Message = message,
            Status = new Success(),
            Data = data
        };
    }

    public static Response Success(string? message = null)
    {
        return new Response
        {
            Message = message,
            Status = new Success(),
            Data = null
        };
    }

    public static Response Fail(Status status, string? message = null)
    {
        return new Response
        {
            Message = message,
            Status = status,
            Data = null
        };
    }
}
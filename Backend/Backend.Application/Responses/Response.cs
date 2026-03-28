using System.Text.Json.Serialization;
using Backend.Application.Errors;


namespace Backend.Application.Responses;

public class Response
{
    public bool IsSuccess => Error is null;
    public string? Message { get; set; }
    public string? Code => Error?.ToString();
    public object? Data { get; init; }
    [JsonIgnore] public Error? Error { get; set; }

    private Response()
    {
    }

    public static Response Success<TData>(TData data, string? message = null) where TData : class
    {
        return new Response
        {
            Message = message,
            Error = null,
            Data = data
        };
    }

    public static Response Success(string? message = null)
    {
        return new Response
        {
            Message = message,
            Error = null,
            Data = null
        };
    }

    public static Response Fail(Error error, string? message = null)
    {
        return new Response
        {
            Message = message,
            Error = error,
            Data = null
        };
    }
}







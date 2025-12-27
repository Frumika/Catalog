namespace Backend.Application.DTO.Responses;

public abstract class BaseResponse<TCode, TResponse>
    where TCode : Enum
    where TResponse : BaseResponse<TCode, TResponse>, new()
{
    public bool IsSuccess { get; set; }
    public string? Message { get; set; }
    public TCode? Code { get; set; }
    public object? Data { get; set; }


    protected static TResponse CreateSuccess<TData>(TCode code, TData? data = null, string? message = null)
        where TData : class
    {
        return new TResponse
        {
            IsSuccess = true,
            Message = message,
            Code = code,
            Data = data
        };
    }

    protected static TResponse CreateSuccess(TCode code, string? message = null)
    {
        return new TResponse
        {
            IsSuccess = true,
            Message = message,
            Code = code,
            Data = null
        };
    }

    public static TResponse Success<TData>(TData? data = null, string? message = null) where TData : class
    {
        return CreateSuccess(default!, data, message);
    }

    public static TResponse Success(string? message = null)
    {
        return CreateSuccess(default!, message);
    }

    public static TResponse Fail(TCode code, string? message = null)
    {
        return new TResponse
        {
            IsSuccess = false,
            Message = message,
            Code = code,
            Data = null
        };
    }
}
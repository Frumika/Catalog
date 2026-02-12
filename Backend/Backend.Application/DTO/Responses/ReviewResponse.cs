using Backend.Application.StatusCodes;


namespace Backend.Application.DTO.Responses;

public class ReviewResponse : BaseResponse<ReviewStatusCode, ReviewResponse>
{
    public new static ReviewResponse Success<TData>(TData? data = null, string? message = null) where TData : class
    {
        return CreateSuccess(ReviewStatusCode.Success, data, message);
    }

    public new static ReviewResponse Success(string? message = null)
    {
        return CreateSuccess(ReviewStatusCode.Success, message);
    }
}
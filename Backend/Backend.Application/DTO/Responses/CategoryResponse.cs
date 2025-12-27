using Backend.Application.StatusCodes;


namespace Backend.Application.DTO.Responses;

public class CategoryResponse : BaseResponse<CategoryStatusCode, CategoryResponse>
{
    public new static CategoryResponse Success<TData>(TData? data = null, string? message = null) where TData : class
    {
        return CreateSuccess(CategoryStatusCode.Success, data, message);
    }

    public new static CategoryResponse Success(string? message = null)
    {
        return CreateSuccess(CategoryStatusCode.Success, message);
    }
}
using Backend.Application.StatusCodes;

namespace Backend.Application.DTO.Responses;

public class ProductResponse : BaseResponse<ProductStatusCode, ProductResponse>
{
    public new static ProductResponse Success<TData>(TData? data = null, string? message = null) where TData : class
    {
        return CreateSuccess(ProductStatusCode.Success, data, message);
    }
    
    public new static ProductResponse Success(string? message = null)
    {
        return CreateSuccess(ProductStatusCode.Success, message);
    }
}
using Backend.Application.StatusCodes;

namespace Backend.Application.DTO.Responses;

public class ProductResponse : BaseResponse<CatalogStatusCode, ProductResponse>
{
    public new static ProductResponse Success<TData>(TData? data = null, string? message = null) where TData : class
    {
        return CreateSuccess(CatalogStatusCode.Success, data, message);
    }
    
    public new static ProductResponse Success(string? message = null)
    {
        return CreateSuccess(CatalogStatusCode.Success, message);
    }
}
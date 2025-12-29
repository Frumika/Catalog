using Backend.Application.StatusCodes;

namespace Backend.Application.DTO.Responses;

public class CatalogResponse : BaseResponse<CatalogStatusCode, CatalogResponse>
{
    public new static CatalogResponse Success<TData>(TData? data = null, string? message = null) where TData : class
    {
        return CreateSuccess(CatalogStatusCode.Success, data, message);
    }
    
    public new static CatalogResponse Success(string? message = null)
    {
        return CreateSuccess(CatalogStatusCode.Success, message);
    }
}
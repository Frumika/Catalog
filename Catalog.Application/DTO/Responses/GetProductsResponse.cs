using Catalog.Application.DTO.Entities;
using Catalog.Application.Enums;

namespace Catalog.Application.DTO.Responses;

public class GetProductsResponse : BaseResponse<GetProductsStatus>
{
    public List<ProductDto>? Products { get; set; }

    public static GetProductsResponse Success(IEnumerable<ProductDto> products, string? message = null)
    {
        return new()
        {
            IsSuccess = true,
            Message = message,
            Code = GetProductsStatus.Success,
            Products = products.ToList()
        };
    }

    public static GetProductsResponse Fail(GetProductsStatus code, string? message = null)
    {
        return new()
        {
            IsSuccess = false,
            Message = message,
            Code = code,
            Products = null
        };
    }
}
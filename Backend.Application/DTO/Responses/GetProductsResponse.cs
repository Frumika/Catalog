using Backend.Application.DTO.Entities;
using Backend.Application.Enums;

namespace Backend.Application.DTO.Responses;

public class GetProductsResponse : BaseResponse<GetProductsStatus>
{
    public List<ProductDto>? Products { get; set; }
    public int TotalCount { get; set; }

    public static GetProductsResponse Success(IEnumerable<ProductDto> products, int? totalCount = null, string? message = null)
    {
        return new()
        {
            IsSuccess = true,
            Message = message,
            Code = GetProductsStatus.Success,
            Products = products.ToList(),
            TotalCount = totalCount ?? 0
        };
    }

    public static GetProductsResponse Fail(GetProductsStatus code, string? message = null)
    {
        return new()
        {
            IsSuccess = false,
            Message = message,
            Code = code,
            Products = null,
            TotalCount = 0
        };
    }
}
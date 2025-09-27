using Catalog.Application.DTO.Entities;
using Catalog.Application.Enums;

namespace Catalog.Application.DTO.Responses;

public class GetCategoriesResponse : BaseResponse<GetCategoriesStatus>
{
    public List<CategoryDto>? Categories { get; set; }


    public static GetCategoriesResponse Success(IEnumerable<CategoryDto> categories, string? message = null)
    {
        return new()
        {
            IsSuccess = true,
            Message = message,
            Code = GetCategoriesStatus.Success,
            Categories = categories.ToList(),
        };
    }

    public static GetCategoriesResponse Fail(GetCategoriesStatus code, string? message = null)
    {
        return new()
        {
            IsSuccess = false,
            Message = message,
            Code = code,
            Categories = null
        };
    }
}
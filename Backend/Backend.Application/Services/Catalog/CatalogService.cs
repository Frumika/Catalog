using Backend.Application.Common;
using Backend.Application.Common.Base;
using Backend.Application.Common.Statuses;
using Backend.Application.Services.Catalog.Dtos;
using Backend.Application.Services.Catalog.Requests;


namespace Backend.Application.Services.Catalog;

public class CatalogService
{
    private readonly ICatalogRepository _catalogRepository;

    public CatalogService(ICatalogRepository catalogRepository)
    {
        _catalogRepository = catalogRepository;
    }


    public async Task<Response> GetProductByIdAsync(int id)
    {
        if (id <= 0) return Response.Fail(new BadRequest(), "Id must be greater than 0");

        try
        {
            ProductExtendedDto? product = await _catalogRepository.GetProductById(id);

            return product is not null
                ? Response.Success(product)
                : Response.Fail(new ProductNotFound(), "The product wasn't found");
        }
        catch (Exception)
        {
            return Response.Fail(new UnknownError(), "Internal server error");
        }
    }

    public async Task<Response> GetProductListAsync(GetProductListRequest request)
    {
        ValidationResult result = request.Validate();
        if (!result.IsValid)
            return Response.Fail(new BadRequest(), result.Message);

        try
        {
            int totalCount = await _catalogRepository.GetProductCount(request.CategoryId);

            List<ProductDto> products = await _catalogRepository
                .GetProducts(request.PageNumber, request.PageSize, request.CategoryId);

            return Response.Success(
                new PagedResultDto<ProductDto>
                {
                    Items = products,
                    TotalCount = totalCount,
                    PageNumber = request.PageNumber,
                    PageSize = request.PageSize
                }
            );
        }
        catch (Exception)
        {
            return Response.Fail(new UnknownError(), "Internal server error");
        }
    }

    public async Task<Response> GetCategoryListAsync()
    {
        try
        {
            List<CategoryDto> categories = await _catalogRepository.GetCategories();
            return Response.Success(new CategoryListDto(categories));
        }
        catch (Exception)
        {
            return Response.Fail(new UnknownError(), "Internal server error");
        }
    }
}
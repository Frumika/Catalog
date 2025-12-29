using Backend.Application.DTO.Entities.Category;
using Backend.Application.DTO.Entities.Product;
using Backend.Application.DTO.Requests.Base;
using Backend.Application.DTO.Requests.Product;
using Backend.Application.DTO.Responses;
using Backend.Application.Services.Interfaces;
using Backend.Application.StatusCodes;
using Backend.DataAccess.Contexts;
using Backend.Domain.Models;
using Microsoft.EntityFrameworkCore;


namespace Backend.Application.Services;

public class CatalogService : ICatalogService
{
    private readonly MainDbContext _dbContext;

    public CatalogService(MainDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<ProductResponse> GetProductByIdAsync(int id)
    {
        if (id <= 0) return ProductResponse.Fail(CatalogStatusCode.BadRequest, "Id must be greater than 0");

        try
        {
            Product? product = await _dbContext.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);
            if (product is null)
                return ProductResponse.Fail(CatalogStatusCode.NotFound, "Product not found");

            return ProductResponse.Success(new ProductDto(product));
        }
        catch (Exception)
        {
            return ProductResponse.Fail(CatalogStatusCode.UnknownError, "Internal server error");
        }
    }

    public async Task<ProductResponse> GetProductListAsync(GetProductListRequest request)
    {
        ValidationResult validationResult = request.Validate();
        if (!validationResult.IsValid)
            return ProductResponse.Fail(CatalogStatusCode.BadRequest, validationResult.Message);

        try
        {
            IQueryable<Product> query = _dbContext.Products.AsNoTracking();

            if (request.CategoryId is not null)
            {
                bool isCategoryExist = await _dbContext.Categories
                    .AnyAsync(category => category.Id == request.CategoryId);

                if (!isCategoryExist)
                    return ProductResponse.Fail(CatalogStatusCode.IncorrectCategory, "Category not found");

                query = query.Where(product => product.CategoryId == request.CategoryId);
            }

            int totalCount = await query.CountAsync();

            List<ProductDto> products = await query
                .OrderBy(product => product.Id)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(product => new ProductDto(product))
                .ToListAsync();

            return ProductResponse.Success(new ProductListDto(products, totalCount));
        }
        catch (Exception)
        {
            return ProductResponse.Fail(CatalogStatusCode.UnknownError, "Internal server error");
        }
    }

    public async Task<ProductResponse> GetCategoryListAsync()
    {
        try
        {
            var categories = await _dbContext.Categories
                .AsNoTracking()
                .OrderBy(category => category.Id)
                .Select(category => new CategoryDto(category))
                .ToListAsync();

            return ProductResponse.Success(new CategoryListDto(categories));
        }
        catch (Exception)
        {
            return ProductResponse.Fail(CatalogStatusCode.UnknownError, "Internal server error");
        }
    }
}
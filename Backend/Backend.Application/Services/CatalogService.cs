using Backend.Application.DTO.Entities.Catalog;
using Backend.Application.DTO.Requests.Base;
using Backend.Application.DTO.Requests.Catalog;
using Backend.Application.DTO.Responses;
using Backend.Application.StatusCodes;
using Backend.DataAccess.Postgres.Contexts;
using Backend.Domain.Models;
using Microsoft.EntityFrameworkCore;


namespace Backend.Application.Services;

public class CatalogService
{
    private const string ImageUrlPrefix = "http://localhost:5780/";
    private readonly MainDbContext _dbContext;

    public CatalogService(MainDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<CatalogResponse> GetProductByIdAsync(int id)
    {
        if (id <= 0) return CatalogResponse.Fail(CatalogStatusCode.BadRequest, "Id must be greater than 0");

        try
        {
            ProductExtendedDto? product = await _dbContext.Products
                .AsNoTracking()
                .Where(p => p.Id == id)
                .Select(p => new ProductExtendedDto
                {
                    Id = p.Id,
                    ProductName = p.Name,
                    Price = p.Price,
                    ProductDescription = p.Description,
                    MakerName = p.Maker.Name,
                    MakerDescription = p.Maker.Description,
                    ImageUrls = p.ProductImages
                        .OrderBy(pi => pi.Position)
                        .Select(pi => ImageUrlPrefix + pi.Path)
                        .ToList()
                })
                .FirstOrDefaultAsync();

            return product is not null
                ? CatalogResponse.Success(product)
                : CatalogResponse.Fail(CatalogStatusCode.ProductNotFound, "The product wasn't found");
        }
        catch (Exception)
        {
            return CatalogResponse.Fail(CatalogStatusCode.UnknownError, "Internal server error");
        }
    }

    public async Task<CatalogResponse> GetProductListAsync(GetProductListRequest request)
    {
        ValidationResult validationResult = request.Validate();
        if (!validationResult.IsValid)
            return CatalogResponse.Fail(CatalogStatusCode.BadRequest, validationResult.Message);

        try
        {
            IQueryable<Product> query = _dbContext.Products.AsNoTracking();

            if (request.CategoryId is not null)
            {
                bool isCategoryExist = await _dbContext.Categories
                    .AnyAsync(category => category.Id == request.CategoryId);

                if (!isCategoryExist)
                    return CatalogResponse.Fail(CatalogStatusCode.IncorrectCategory, "Category not found");

                query = query.Where(product => product.CategoryId == request.CategoryId);
            }

            int totalCount = await query.CountAsync();

            List<ProductDto> products = await query
                .AsNoTracking()
                .OrderBy(p => p.Id)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(p => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = p.Price,
                    ImageUrl = p.ProductImages
                        .OrderBy(pi => pi.Position)
                        .Select(pi => ImageUrlPrefix + pi.Path)
                        .FirstOrDefault() ?? string.Empty
                })
                .ToListAsync();

            return CatalogResponse.Success(new ProductListDto(products, totalCount));
        }
        catch (Exception)
        {
            return CatalogResponse.Fail(CatalogStatusCode.UnknownError, "Internal server error");
        }
    }

    public async Task<CatalogResponse> GetCategoryListAsync()
    {
        try
        {
            var categories = await _dbContext.Categories
                .AsNoTracking()
                .OrderBy(category => category.Id)
                .Select(category => new CategoryDto(category))
                .ToListAsync();

            return CatalogResponse.Success(new CategoryListDto(categories));
        }
        catch (Exception)
        {
            return CatalogResponse.Fail(CatalogStatusCode.UnknownError, "Internal server error");
        }
    }
}
using Backend.Application.Common;
using Backend.Application.Common.Base;
using Backend.Application.Common.Statuses;
using Backend.Application.DataAccess.Contexts;
using Backend.Application.Services.Catalog.Dtos;
using Backend.Application.Services.Catalog.Requests;
using Backend.Domain.Models;
using Microsoft.EntityFrameworkCore;


namespace Backend.Application.Services.Catalog;

public class CatalogService
{
    private readonly MainDbContext _dbContext;

    public CatalogService(MainDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<Response> GetProductByIdAsync(int id)
    {
        if (id <= 0) return Response.Fail(new BadRequest(), "Id must be greater than 0");

        try
        {
            ProductExtendedDto? product = await _dbContext.Products
                .AsNoTracking()
                .Where(p => p.Id == id)
                .Select(p => new ProductExtendedDto
                {
                    ProductId = p.Id,
                    ProductName = p.Name,
                    Price = (int)Math.Round(p.Price, 0),
                    DiscountPrice = (int)Math.Round(p.Price * (100 - p.DiscountPercent) / 100m, 0),
                    DiscountPercent = p.DiscountPercent,
                    ReviewCount = p.Reviews.Count,
                    AverageScore = p.Reviews.Any() ? Math.Round(p.Reviews.Average(r => r.Score), 1) : 0,
                    ProductDescription = p.Description,
                    MakerName = p.Seller.Name,
                    MakerDescription = p.Seller.Description,
                    ImageUrls = p.ProductImages
                        .OrderBy(pi => pi.Position)
                        .Select(pi => pi.Path)
                        .ToList()
                })
                .FirstOrDefaultAsync();

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
            IQueryable<Product> query = _dbContext.Products.AsNoTracking();

            if (request.CategoryId is not null)
            {
                bool isCategoryExist = await _dbContext.Categories
                    .AnyAsync(category => category.Id == request.CategoryId);

                if (!isCategoryExist)
                    return Response.Fail(new IncorrectCategory(), "Category not found");

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
                    ProductId = p.Id,
                    ProductName = p.Name,
                    Price = (int)Math.Round(p.Price, 0),
                    DiscountPrice = (int)Math.Round(p.Price * (100 - p.DiscountPercent) / 100m, 0),
                    DiscountPercent = p.DiscountPercent,
                    ReviewCount = p.Reviews.Count,
                    AverageScore = p.Reviews.Any() ? Math.Round(p.Reviews.Average(r => r.Score), 1) : 0,
                    ImageUrl = p.ProductImages
                        .OrderBy(pi => pi.Position)
                        .Select(pi => pi.Path)
                        .FirstOrDefault()
                })
                .ToListAsync();

            return Response.Success(
                new PaginatedResultDto<ProductDto>
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
            var categories = await _dbContext.Categories
                .AsNoTracking()
                .OrderBy(category => category.Id)
                .Select(category => new CategoryDto(category))
                .ToListAsync();

            return Response.Success(new CategoryListDto(categories));
        }
        catch (Exception)
        {
            return Response.Fail(new UnknownError(), "Internal server error");
        }
    }
}
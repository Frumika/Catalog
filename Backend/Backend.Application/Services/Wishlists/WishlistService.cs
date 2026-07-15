using Backend.Application.Common;
using Backend.Application.Common.Base;
using Backend.Application.Common.Statuses;
using Backend.Application.DataAccess.Contexts;
using Backend.Application.Services.Catalog.Dtos;
using Backend.Application.Services.Wishlists.Dtos;
using Backend.Application.Services.Wishlists.Requests;
using Backend.Domain.Models;
using Microsoft.EntityFrameworkCore;


namespace Backend.Application.Services.Wishlists;

public class WishlistService
{
    private readonly MainDbContext _dbContext;

    public WishlistService(MainDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<Response> GetWishlistPreviewAsync(int userId)
    {
        try
        {
            int wishlistId = await _dbContext.Wishlists
                .AsNoTracking()
                .Where(wi => wi.UserId == userId)
                .Select(wi => wi.Id)
                .FirstAsync();

            List<WishedProductDto> ids = await _dbContext.WishlistItems
                .AsNoTracking()
                .Where(wi => wi.WishlistId == wishlistId)
                .Select(wi => new WishedProductDto
                {
                    ProductId = wi.ProductId,
                })
                .ToListAsync();

            return Response.Success(new { Items = ids });
        }
        catch (Exception)
        {
            return Response.Fail(new UnknownError(), "Internal server error");
        }
    }

    public async Task<Response> GetWishlistAsync(int userId, GetWishlistRequest request)
    {
        ValidationResult result = request.Validate();
        if (!result.IsValid) return Response.Fail(new BadRequest(), result.Message);

        try
        {
            int wishlistId = await _dbContext.Wishlists
                .AsNoTracking()
                .Where(wi => wi.UserId == userId)
                .Select(wi => wi.Id)
                .FirstAsync();

            int totalCount = await _dbContext.WishlistItems
                .AsNoTracking()
                .Where(wi => wi.WishlistId == wishlistId)
                .CountAsync();

            List<ProductDto> products = await _dbContext.WishlistItems
                .AsNoTracking()
                .Where(wi => wi.WishlistId == wishlistId)
                .OrderByDescending(wi => wi.AddedAt)
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(wi => new ProductDto
                {
                    ProductId = wi.ProductId,
                    ProductName = wi.Product.Name,
                    Price = (int)Math.Round(wi.Product.Price, 0),
                    DiscountPrice =
                        (int)Math.Round(wi.Product.Price * (100 - wi.Product.DiscountPercent) / 100m, 0),
                    DiscountPercent = wi.Product.DiscountPercent,
                    ReviewCount = wi.Product.Reviews.Count,
                    AverageScore = wi.Product.Reviews.Any()
                        ? Math.Round(wi.Product.Reviews.Average(r => r.Score), 1)
                        : 0,
                    ImageUrl = wi.Product.ProductImages
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

    public async Task<Response> AddProductAsync(int userId, AddProductRequest request)
    {
        ValidationResult result = request.Validate();
        if (!result.IsValid)
            return Response.Fail(new BadRequest(), result.Message);

        try
        {
            int wishlistId = await _dbContext.Wishlists
                .AsNoTracking()
                .Where(w => w.UserId == userId)
                .Select(w => w.Id)
                .FirstAsync();

            int? productId = await _dbContext.Products
                .AsNoTracking()
                .Where(p => p.Id == request.ProductId)
                .Select(p => (int?)p.Id)
                .FirstOrDefaultAsync();
            if (productId is null)
                return Response.Fail(new ProductNotFound(), "The product wasn't found");

            WishlistItem? wishedProduct = await _dbContext.WishlistItems
                .FirstOrDefaultAsync(wi => wi.WishlistId == wishlistId && wi.ProductId == request.ProductId);

            if (wishedProduct is null)
            {
                wishedProduct = new WishlistItem
                {
                    WishlistId = wishlistId,
                    ProductId = request.ProductId,
                    AddedAt = DateTime.UtcNow
                };

                _dbContext.WishlistItems.Add(wishedProduct);
                await _dbContext.SaveChangesAsync();
            }

            return Response.Success(new WishedProductDto
            {
                ProductId = wishedProduct.ProductId
            });
        }
        catch (Exception)
        {
            return Response.Fail(new UnknownError(), "Internal server error");
        }
    }

    public async Task<Response> RemoveProductAsync(int userId, RemoveProductRequest request)
    {
        ValidationResult result = request.Validate();
        if (!result.IsValid)
            return Response.Fail(new BadRequest(), result.Message);

        try
        {
            int wishlistId = await _dbContext.Carts
                .AsNoTracking()
                .Where(c => c.UserId == userId)
                .Select(c => c.Id)
                .FirstAsync();

            WishlistItem? wishedProduct = await _dbContext.WishlistItems
                .FirstOrDefaultAsync(wi => wi.WishlistId == wishlistId && wi.ProductId == request.ProductId);
            if (wishedProduct is not null)
            {
                _dbContext.WishlistItems.Remove(wishedProduct);
                await _dbContext.SaveChangesAsync();
            }

            return Response.Success(new WishedProductDto
            {
                ProductId = wishedProduct?.ProductId ?? request.ProductId
            });
        }
        catch (Exception)
        {
            return Response.Fail(new UnknownError(), "Internal server error");
        }
    }
}
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

            List<WishedProductDto> ids = await _dbContext.WishedProducts
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

            WishedProduct? wishedProduct = await _dbContext.WishedProducts
                .FirstOrDefaultAsync(wi => wi.WishlistId == wishlistId && wi.ProductId == request.ProductId);

            if (wishedProduct is null)
            {
                wishedProduct = new WishedProduct
                {
                    WishlistId = wishlistId,
                    ProductId = request.ProductId,
                    AddedAt = DateTime.UtcNow
                };

                _dbContext.WishedProducts.Add(wishedProduct);
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

            WishedProduct? wishedProduct = await _dbContext.WishedProducts
                .FirstOrDefaultAsync(wi => wi.WishlistId == wishlistId && wi.ProductId == request.ProductId);
            if (wishedProduct is not null)
            {
                _dbContext.WishedProducts.Remove(wishedProduct);
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
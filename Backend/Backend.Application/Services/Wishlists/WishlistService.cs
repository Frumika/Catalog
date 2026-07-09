using Backend.Application.Common;
using Backend.Application.Common.Base;
using Backend.Application.Common.Statuses;
using Backend.Application.DataAccess.Contexts;
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

    public async Task<Response> GetWishlistAsync(int userId)
    {
        try
        {
            int wishlistId = await _dbContext.Wishlists
                .AsNoTracking()
                .Where(wi => wi.UserId == userId)
                .Select(wi => wi.Id)
                .FirstAsync();

            List<WishlistItemDto> wishlistItems = await _dbContext.WishlistItems
                .AsNoTracking()
                .Where(wi => wi.WishlistId == wishlistId)
                .OrderBy(wi => wi.AddedAt)
                .Select(wi => new WishlistItemDto
                {
                    ProductId = wi.ProductId,
                    ProductName = wi.Product.Name,
                    ProductPrice = wi.Product.Price
                })
                .ToListAsync();

            return Response.Success(new WishlistDto { WishlistItems = wishlistItems });
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

            bool isCartItemExists = await _dbContext.WishlistItems
                .AnyAsync(wi => wi.WishlistId == wishlistId && wi.ProductId == request.ProductId);
            if (!isCartItemExists)
            {
                _dbContext.WishlistItems.Add(
                    new WishlistItem
                    {
                        WishlistId = wishlistId,
                        ProductId = request.ProductId,
                        AddedAt = DateTime.UtcNow
                    }
                );
                await _dbContext.SaveChangesAsync();
            }

            return Response.Success("The product was added");
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

            WishlistItem? cartItem = await _dbContext.WishlistItems
                .FirstOrDefaultAsync(wi => wi.WishlistId == wishlistId && wi.ProductId == request.ProductId);
            if (cartItem != null)
            {
                _dbContext.WishlistItems.Remove(cartItem);
                await _dbContext.SaveChangesAsync();
            }

            return Response.Success("The product was deleted");
        }
        catch (Exception)
        {
            return Response.Fail(new UnknownError(), "Internal server error");
        }
    }
}
using Backend.Application.Common;
using Backend.Application.Common.Base;
using Backend.Application.Common.Statuses;
using Backend.Application.DataAccess.Contexts;
using Backend.Application.Services.Carts.Dtos;
using Backend.Application.Services.Carts.Requests;
using Backend.Domain.Models;
using Microsoft.EntityFrameworkCore;


namespace Backend.Application.Services.Carts;

public class CartService
{
    private readonly MainDbContext _dbContext;

    public CartService(MainDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Response> GetCartAsync(GetCartRequest request)
    {
        ValidationResult result = request.Validate();
        if (!result.IsValid)
            return Response.Fail(new BadRequest(), result.Message);

        try
        {
            int? userId = await _dbContext.UserSessions
                .AsNoTracking()
                .Where(ui => ui.UId == request.UserSessionId)
                .Select(ui => (int?)ui.UserId)
                .FirstOrDefaultAsync();
            if (userId is null)
                return Response.Fail(new UserNotFound(), "The user wasn't found");

            int cartId = await _dbContext.Carts
                .AsNoTracking()
                .Where(c => c.UserId == userId)
                .Select(c => c.Id)
                .FirstAsync();

            List<CartPositionDto> cartItems = await _dbContext.CartItems
                .AsNoTracking()
                .Where(ci => ci.CartId == cartId)
                .OrderBy(ci => ci.AddedAt)
                .Select(ci => new CartPositionDto
                {
                    ProductId = ci.ProductId,
                    ProductName = ci.Product.Name,
                    Quantity = ci.Quantity,

                    BasePrice = (int)Math.Round(ci.Product.Price, 0),
                    DiscountPercent = ci.Product.DiscountPercent,
                    PriceWithDiscount =
                        (int)Math.Round(ci.Product.Price * (100 - ci.Product.DiscountPercent) / 100m, 0),

                    ProductImage = ci.Product.ProductImages
                        .OrderBy(pi => pi.Position)
                        .Select(pi => pi.Path)
                        .FirstOrDefault()
                })
                .ToListAsync();

            return Response.Success(
                new CartDto
                {
                    Items = cartItems,
                    TotalQuantity = cartItems.Sum(c => c.Quantity),
                    TotalBasePrice = cartItems.Sum(c => c.PositionBaseTotal),
                    TotalDiscountAmount = cartItems.Sum(c => c.PositionDiscountAmount)
                }
            );
        }
        catch (Exception)
        {
            return Response.Fail(new UnknownError(), "Internal server error");
        }
    }

    public async Task<Response> AddProductAsync(AddProductRequest request)
    {
        ValidationResult result = request.Validate();
        if (!result.IsValid)
            return Response.Fail(new BadRequest(), result.Message);

        try
        {
            int? userId = await _dbContext.UserSessions
                .AsNoTracking()
                .Where(ui => ui.UId == request.UserSessionId)
                .Select(ui => (int?)ui.UserId)
                .FirstOrDefaultAsync();
            if (userId is null)
                return Response.Fail(new UserNotFound(), "The user wasn't found");

            int cartId = await _dbContext.Carts
                .AsNoTracking()
                .Where(c => c.UserId == userId)
                .Select(c => c.Id)
                .FirstAsync();

            int? productId = await _dbContext.Products
                .AsNoTracking()
                .Where(p => p.Id == request.ProductId)
                .Select(p => (int?)p.Id)
                .FirstOrDefaultAsync();
            if (productId is null)
                return Response.Fail(new ProductNotFound(), "The product wasn't found");

            bool isCartItemExists = await _dbContext.CartItems
                .AnyAsync(ci => ci.CartId == cartId && ci.ProductId == request.ProductId);
            if (!isCartItemExists)
            {
                _dbContext.CartItems.Add(
                    new CartItem
                    {
                        CartId = cartId,
                        ProductId = request.ProductId,
                        Quantity = 1,
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

    public async Task<Response> UpdateProductQuantityAsync(UpdateProductQuantityRequest request)
    {
        ValidationResult result = request.Validate();
        if (!result.IsValid)
            return Response.Fail(new BadRequest(), result.Message);

        if (request.Quantity == 0) return await RemoveProductAsync(new RemoveProductRequest(request));

        int? userId = await _dbContext.UserSessions
            .AsNoTracking()
            .Where(ui => ui.UId == request.UserSessionId)
            .Select(ui => (int?)ui.UserId)
            .FirstOrDefaultAsync();
        if (userId is null)
            return Response.Fail(new UserNotFound(), "The user wasn't found");

        int cartId = await _dbContext.Carts
            .AsNoTracking()
            .Where(c => c.UserId == userId)
            .Select(c => c.Id)
            .FirstAsync();

        CartItem? cartItem = await _dbContext.CartItems
            .FirstOrDefaultAsync(ci => ci.CartId == cartId && ci.ProductId == request.ProductId);
        if (cartItem is null)
            return Response.Fail(new ProductNotFound(), "The product in the cart wasn't found");

        if (cartItem.Quantity != request.Quantity)
        {
            cartItem.Quantity = request.Quantity;
            await _dbContext.SaveChangesAsync();
        }

        return Response.Success("Product quantity was updated");
    }

    public async Task<Response> RemoveProductAsync(RemoveProductRequest request)
    {
        ValidationResult result = request.Validate();
        if (!result.IsValid)
            return Response.Fail(new BadRequest(), result.Message);

        try
        {
            int? userId = await _dbContext.UserSessions
                .AsNoTracking()
                .Where(ui => ui.UId == request.UserSessionId)
                .Select(ui => (int?)ui.UserId)
                .FirstOrDefaultAsync();
            if (userId is null)
                return Response.Fail(new UserNotFound(), "User wasn't found");

            int cartId = await _dbContext.Carts
                .AsNoTracking()
                .Where(c => c.UserId == userId)
                .Select(c => c.Id)
                .FirstAsync();

            CartItem? cartItem = await _dbContext.CartItems
                .FirstOrDefaultAsync(ci => ci.CartId == cartId && ci.ProductId == request.ProductId);
            if (cartItem is not null)
            {
                _dbContext.CartItems.Remove(cartItem);
                await _dbContext.SaveChangesAsync();
            }

            return Response.Success("The product was deleted");
        }
        catch (Exception)
        {
            return Response.Fail(new UnknownError(), "Internal server error");
        }
    }

    public async Task<Response> ClearCartAsync(DeleteCartRequest request)
    {
        ValidationResult result = request.Validate();
        if (!result.IsValid)
            return Response.Fail(new BadRequest(), result.Message);

        try
        {
            int? userId = await _dbContext.UserSessions
                .AsNoTracking()
                .Where(ui => ui.UId == request.UserSessionId)
                .Select(ui => (int?)ui.UserId)
                .FirstOrDefaultAsync();
            if (userId is null)
                return Response.Fail(new UserNotFound(), "User session wasn't found");

            int cartId = await _dbContext.Carts
                .AsNoTracking()
                .Where(c => c.UserId == userId)
                .Select(c => c.Id)
                .FirstOrDefaultAsync();

            await _dbContext.CartItems
                .Where(ci => ci.CartId == cartId)
                .ExecuteDeleteAsync();

            return Response.Success("The cart was deleted");
        }
        catch (Exception)
        {
            return Response.Fail(new UnknownError(), "Internal server error");
        }
    }
}
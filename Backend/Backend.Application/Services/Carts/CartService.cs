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

    public async Task<Response> GetCartPreviewAsync(int userId)
    {
        try
        {
            int cartId = await _dbContext.Carts
                .AsNoTracking()
                .Where(c => c.UserId == userId)
                .Select(c => c.Id)
                .FirstAsync();

            List<CartPositionDto> cartPositions = await _dbContext.CartItems
                .AsNoTracking()
                .Where(ci => ci.CartId == cartId)
                .OrderBy(ci => ci.AddedAt)
                .Select(ci => new CartPositionDto
                    {
                        ProductId = ci.ProductId,
                        Quantity = ci.Quantity,
                    }
                ).ToListAsync();

            return Response.Success(
                new CartDto<CartPositionDto>
                {
                    Items = cartPositions,
                    TotalQuantity = cartPositions.Sum(c => c.Quantity),
                }
            );
        }
        catch (Exception)
        {
            return Response.Fail(new UnknownError(), "Internal server error");
        }
    }

    public async Task<Response> GetCartAsync(int userId)
    {
        try
        {
            int cartId = await _dbContext.Carts
                .AsNoTracking()
                .Where(c => c.UserId == userId)
                .Select(c => c.Id)
                .FirstAsync();

            List<CartPositionExtendedDto> cartItems = await _dbContext.CartItems
                .AsNoTracking()
                .Where(ci => ci.CartId == cartId)
                .OrderBy(ci => ci.AddedAt)
                .Select(ci => new CartPositionExtendedDto
                {
                    ProductId = ci.ProductId,
                    ProductName = ci.Product.Name,
                    Quantity = ci.Quantity,

                    BasePrice = (int)Math.Round(ci.Product.Price, 0),
                    DiscountPercent = ci.Product.DiscountPercent,
                    PriceWithDiscount =
                        (int)Math.Round(ci.Product.Price * (100 - ci.Product.DiscountPercent) / 100m, 0),

                    ImageUrl = ci.Product.ProductImages
                        .OrderBy(pi => pi.Position)
                        .Select(pi => pi.Path)
                        .FirstOrDefault()
                })
                .ToListAsync();

            return Response.Success(
                new CartExtendedDto
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

    public async Task<Response> AddProductAsync(int userId, AddProductRequest request)
    {
        ValidationResult result = request.Validate();
        if (!result.IsValid)
            return Response.Fail(new BadRequest(), result.Message);

        try
        {
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

            CartItem? cartItem = await _dbContext.CartItems
                .Where(ci => ci.CartId == cartId && ci.ProductId == request.ProductId)
                .FirstOrDefaultAsync();
            if (cartItem is null)
            {
                cartItem = new CartItem
                {
                    CartId = cartId,
                    ProductId = request.ProductId,
                    Quantity = 1,
                    AddedAt = DateTime.UtcNow
                };

                _dbContext.CartItems.Add(cartItem);
                await _dbContext.SaveChangesAsync();
            }


            CartPositionDto cartPosition = new()
            {
                ProductId = cartItem.ProductId,
                Quantity = cartItem.Quantity
            };

            return Response.Success(cartPosition, "The product was added");
        }
        catch (Exception)
        {
            return Response.Fail(new UnknownError(), "Internal server error");
        }
    }

    public async Task<Response> UpdateProductQuantityAsync(int userId, UpdateProductQuantityRequest request)
    {
        ValidationResult result = request.Validate();
        if (!result.IsValid)
            return Response.Fail(new BadRequest(), result.Message);

        if (request.Quantity == 0) return await RemoveProductAsync(userId, new RemoveProductRequest(request));

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

        CartPositionDto cartPosition = new()
        {
            ProductId = cartItem.ProductId,
            Quantity = cartItem.Quantity
        };

        return Response.Success(cartPosition, "Product quantity was updated");
    }

    public async Task<Response> RemoveProductAsync(int userId, RemoveProductRequest request)
    {
        ValidationResult result = request.Validate();
        if (!result.IsValid)
            return Response.Fail(new BadRequest(), result.Message);

        try
        {
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

            CartPositionDto cartPosition = new()
            {
                ProductId = request.ProductId,
                Quantity = 0
            };

            return Response.Success(cartPosition, "The product was deleted");
        }
        catch (Exception)
        {
            return Response.Fail(new UnknownError(), "Internal server error");
        }
    }

    public async Task<Response> ClearCartAsync(int userId)
    {
        try
        {
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
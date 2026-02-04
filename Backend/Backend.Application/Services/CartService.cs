using Backend.Application.DTO.Entities.Cart;
using Backend.Application.DTO.Requests.Base;
using Backend.Application.DTO.Requests.Cart;
using Backend.Application.DTO.Responses;
using Backend.Application.StatusCodes;
using Backend.DataAccess.Postgres.Contexts;
using Backend.DataAccess.Storages;
using Microsoft.EntityFrameworkCore;
using CartItem = Backend.Domain.Models.CartItem;
using ResponseCartItem = Backend.Application.DTO.Entities.Cart.CartItem;


namespace Backend.Application.Services;

public class CartService
{
    private readonly MainDbContext _dbContext;
    private readonly UserSessionStorage _userStorage;

    public CartService(MainDbContext dbContext, UserSessionStorage userStorage)
    {
        _dbContext = dbContext;
        _userStorage = userStorage;
    }

    public async Task<CartResponse> GetCartAsync(GetCartRequest request)
    {
        ValidationResult validationResult = request.Validate();
        if (!validationResult.IsValid)
            return CartResponse.Fail(CartStatusCode.BadRequest, validationResult.Message);

        try
        {
            int? userId = await _userStorage.GetUserIdAsync(request.UserSessionId);
            if (userId is null)
                return CartResponse.Fail(CartStatusCode.UserSessionNotFound, "The user session wasn't found");

            List<ResponseCartItem> cartItems = await _dbContext.CartItems
                .AsNoTracking()
                .Where(ci => ci.Cart.UserId == userId)
                .Select(ci => new ResponseCartItem
                {
                    ProductId = ci.ProductId,
                    ProductName = ci.Product.Name,
                    Quantity = ci.Quantity,
                    ProductPrice = ci.Product.Price
                })
                .ToListAsync();

            decimal totalPrice = cartItems.Sum(c => c.TotalPrice);

            return CartResponse.Success(new CartDto { CartItems = cartItems, TotalPrice = totalPrice });
        }
        catch (Exception)
        {
            return CartResponse.Fail(CartStatusCode.UnknownError, "Internal server error");
        }
    }

    public async Task<CartResponse> AddProductAsync(AddProductRequest request)
    {
        ValidationResult validationResult = request.Validate();
        if (!validationResult.IsValid)
            return CartResponse.Fail(CartStatusCode.BadRequest, validationResult.Message);

        try
        {
            int? userId = await _userStorage.GetUserIdAsync(request.UserSessionId);
            if (userId is null)
                return CartResponse.Fail(CartStatusCode.UserSessionNotFound, "The user session wasn't found");

            await _userStorage.RefreshSessionTimeAsync(request.UserSessionId);

            int cartId = await _dbContext.Carts
                .Where(c => c.UserId == userId)
                .Select(c => c.Id)
                .FirstAsync();

            int? productId = await _dbContext.Products
                .Where(p => p.Id == request.ProductId)
                .Select(p => (int?)p.Id)
                .FirstOrDefaultAsync();

            if (productId is null)
                return CartResponse.Fail(CartStatusCode.ProductNotFound, "The product wasn't found");

            bool isCartItemExists = await _dbContext.CartItems
                .AnyAsync(ci => ci.CartId == cartId &&
                                ci.ProductId == request.ProductId
                );

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

            return CartResponse.Success("The product was added");
        }
        catch (Exception)
        {
            return CartResponse.Fail(CartStatusCode.UnknownError, "Internal server error");
        }
    }

    public async Task<CartResponse> UpdateProductQuantityAsync(UpdateProductQuantityRequest request)
    {
        ValidationResult validationResult = request.Validate();
        if (!validationResult.IsValid)
            return CartResponse.Fail(CartStatusCode.BadRequest, validationResult.Message);

        if (request.Quantity == 0) return await RemoveProductAsync(new RemoveProductRequest(request));

        int? userId = await _userStorage.GetUserIdAsync(request.UserSessionId);
        if (userId is null)
            return CartResponse.Fail(CartStatusCode.UserSessionNotFound, "The user session wasn't found");

        await _userStorage.RefreshSessionTimeAsync(request.UserSessionId);

        int? cartId = await _dbContext.Carts
            .AsNoTracking()
            .Where(c => c.UserId == userId)
            .Select(c => (int?)c.Id)
            .FirstOrDefaultAsync();
        if (cartId is null)
            return CartResponse.Fail(CartStatusCode.CartStateNotFound, "The cart wasn't found");

        CartItem? cartItem = await _dbContext.CartItems
            .FirstOrDefaultAsync(ci => ci.Cart.UserId == cartId && ci.ProductId == request.ProductId);
        if (cartItem is null)
            return CartResponse.Fail(CartStatusCode.ProductNotFound, "The product in the cart wasn't found");

        if (cartItem.Quantity != request.Quantity)
        {
            cartItem.Quantity = request.Quantity;
            await _dbContext.SaveChangesAsync();
        }

        return CartResponse.Success("Product quantity was updated");
    }

    public async Task<CartResponse> RemoveProductAsync(RemoveProductRequest request)
    {
        ValidationResult validationResult = request.Validate();
        if (!validationResult.IsValid)
            return CartResponse.Fail(CartStatusCode.BadRequest, validationResult.Message);

        try
        {
            int? userId = await _userStorage.GetUserIdAsync(request.UserSessionId);
            if (userId is null)
                return CartResponse.Fail(CartStatusCode.UserSessionNotFound, "User session wasn't found");

            await _userStorage.RefreshSessionTimeAsync(request.UserSessionId);

            int? cartId = await _dbContext.Carts
                .AsNoTracking()
                .Where(c => c.UserId == userId)
                .Select(c => (int?)c.Id)
                .FirstOrDefaultAsync();
            if (cartId is null)
                return CartResponse.Fail(CartStatusCode.CartStateNotFound, "The cart wasn't found");

            CartItem? cartItem = await _dbContext.CartItems
                .FirstOrDefaultAsync(ci => ci.Cart.Id == cartId && ci.ProductId == request.ProductId);
            if (cartItem != null)
            {
                _dbContext.CartItems.Remove(cartItem);
                await _dbContext.SaveChangesAsync();
            }

            return CartResponse.Success("The product was deleted");
        }
        catch (Exception)
        {
            return CartResponse.Fail(CartStatusCode.UnknownError, "Internal server error");
        }
    }

    public async Task<CartResponse> ClearCartAsync(DeleteCartRequest request)
    {
        ValidationResult validationResult = request.Validate();
        if (!validationResult.IsValid)
            return CartResponse.Fail(CartStatusCode.BadRequest, validationResult.Message);

        try
        {
            int? userId = await _userStorage.GetUserIdAsync(request.UserSessionId);
            if (userId is null)
                return CartResponse.Fail(CartStatusCode.UserSessionNotFound, "User session wasn't found");

            await _userStorage.RefreshSessionTimeAsync(request.UserSessionId);

            int? cartId = await _dbContext.Carts
                .AsNoTracking()
                .Where(c => c.UserId == userId)
                .Select(c => (int?)c.Id)
                .FirstOrDefaultAsync();
            if (cartId is null)
                return CartResponse.Fail(CartStatusCode.CartStateNotFound, "The cart wasn't found");

            await _dbContext.CartItems
                .Where(ci => ci.Cart.Id == cartId)
                .ExecuteDeleteAsync();

            return CartResponse.Success("The cart was deleted");
        }
        catch (Exception)
        {
            return CartResponse.Fail(CartStatusCode.UnknownError, "Internal server error");
        }
    }
}
using Backend.Application.Common;
using Backend.Application.Common.Base;
using Backend.Application.Common.Statuses;
using Backend.Application.Services.Carts.Dtos;
using Backend.Application.Services.Carts.Requests;
using Backend.Domain.Models;

namespace Backend.Application.Services.Carts;

public class CartService
{
    private readonly IBaseRepository _baseRepository;
    private readonly ICartRepository _cartRepository;

    public CartService(IBaseRepository baseRepository, ICartRepository cartRepository)
    {
        _baseRepository = baseRepository;
        _cartRepository = cartRepository;
    }

    public async Task<Response> GetCartAsync(GetCartRequest request)
    {
        ValidationResult result = request.Validate();
        if (!result.IsValid)
            return Response.Fail(new BadRequest(), result.Message);

        try
        {
            int? userId = await _baseRepository.GetUserIdAsync(request.UserSessionId);
            if (userId is null)
                return Response.Fail(new UserNotFound(), "The user wasn't found");

            int cartId = await _cartRepository.GetCartIdAsync((int)userId);

            List<CartItemDto> cartItems = await _cartRepository.GetCartItemsAsync(cartId);
            decimal totalPrice = cartItems.Sum(c => c.TotalPrice);

            return Response.Success(new CartDto { CartItems = cartItems, TotalPrice = totalPrice });
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
            int? userId = await _baseRepository.GetUserIdAsync(request.UserSessionId);
            if (userId is null)
                return Response.Fail(new UserNotFound(), "The user wasn't found");

            int cartId = await _cartRepository.GetCartIdAsync((int)userId);

            bool isProductExist = await _cartRepository.IsProductExistAsync(request.ProductId);
            if (!isProductExist)
                return Response.Fail(new ProductNotFound(), "The product wasn't found");

            bool isCartItemExists = await _cartRepository.IsCartItemExistAsync(cartId, request.ProductId);
            if (!isCartItemExists)
            {
                _baseRepository.Add(
                    new CartItem
                    {
                        CartId = cartId,
                        ProductId = request.ProductId,
                        Quantity = 1,
                        AddedAt = DateTime.UtcNow
                    }
                );
                await _baseRepository.CommitAsync();
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

        int? userId = await _baseRepository.GetUserIdAsync(request.UserSessionId);
        if (userId is null)
            return Response.Fail(new UserNotFound(), "The user wasn't found");

        int cartId = await _cartRepository.GetCartIdAsync((int)userId);

        CartItem? cartItem = await _cartRepository.GetCartItemAsync(cartId, request.ProductId);
        if (cartItem is null)
            return Response.Fail(new ProductNotFound(), "The product in the cart wasn't found");

        if (cartItem.Quantity != request.Quantity)
        {
            cartItem.Quantity = request.Quantity;
            await _baseRepository.CommitAsync();
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
            int? userId = await _baseRepository.GetUserIdAsync(request.UserSessionId);
            if (userId is null)
                return Response.Fail(new UserNotFound(), "User wasn't found");

            int cartId = await _cartRepository.GetCartIdAsync((int)userId);

            CartItem? cartItem = await _cartRepository.GetCartItemAsync(cartId, request.ProductId);
            if (cartItem is not null)
            {
                _baseRepository.Remove(cartItem);
                await _baseRepository.CommitAsync();
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
            int? userId = await _baseRepository.GetUserIdAsync(request.UserSessionId);
            if (userId is null)
                return Response.Fail(new UserNotFound(), "User session wasn't found");

            int cartId = await _cartRepository.GetCartIdAsync((int)userId);
            await _cartRepository.ClearCartAsync(cartId);

            return Response.Success("The cart was deleted");
        }
        catch (Exception)
        {
            return Response.Fail(new UnknownError(), "Internal server error");
        }
    }
}
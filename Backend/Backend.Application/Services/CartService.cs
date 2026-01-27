using Backend.Application.DTO.Entities.Cart;
using Backend.Application.DTO.Requests.Base;
using Backend.Application.DTO.Requests.Cart;
using Backend.Application.DTO.Responses;
using Backend.Application.StatusCodes;
using Backend.DataAccess.Postgres.Contexts;
using Backend.DataAccess.Storages;
using Backend.DataAccess.Storages.DTO;
using Backend.Domain.Models;
using Microsoft.EntityFrameworkCore;
using CartItem = Backend.DataAccess.Storages.DTO.CartItem;
using ResponseCartItem = Backend.Application.DTO.Entities.Cart.CartItem;


namespace Backend.Application.Services;

public class CartService
{
    private readonly MainDbContext _dbContext;
    private readonly UserSessionStorage _userStorage;
    private readonly CartStateStorage _cartStorage;

    public CartService(MainDbContext dbContext, CartStateStorage cartStorage, UserSessionStorage userStorage)
    {
        _dbContext = dbContext;
        _cartStorage = cartStorage;
        _userStorage = userStorage;
    }

    public async Task<CartResponse> GetCartAsync(GetCartRequest request)
    {
        ValidationResult validationResult = request.Validate();
        if (!validationResult.IsValid)
            return CartResponse.Fail(CartStatusCode.BadRequest, validationResult.Message);

        try
        {
            UserSessionDto? userSession = await _userStorage.GetSessionAsync(request.UserSessionId);
            if (userSession is null)
                return CartResponse.Fail(CartStatusCode.UserSessionNotFound, "User session wasn't found");

            CartStateDto? cartState = await _cartStorage.GetStateAsync(userSession.UserId);
            if (cartState is null)
                return CartResponse.Success(new CartDto());

            IEnumerable<int> productIds = cartState.Products.Select(i => i.Id);
            Dictionary<int, Product> products = await _dbContext.Products
                .AsNoTracking()
                .Where(p => productIds.Contains(p.Id))
                .ToDictionaryAsync(p => p.Id);


            decimal totalPrice = 0m;
            List<ResponseCartItem> cartItems = new();
            foreach (var cartItem in cartState.Products)
            {
                if (!products.TryGetValue(cartItem.Id, out Product? product))
                    return CartResponse.Fail(CartStatusCode.ProductNotFound, "The product wasn't found");

                ResponseCartItem responseCartItem = new(product, cartItem.Quantity);
                totalPrice += responseCartItem.TotalPrice;

                cartItems.Add(responseCartItem);
            }

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
            UserSessionDto? userSession = await _userStorage.GetSessionAsync(request.UserSessionId);
            if (userSession is null)
                return CartResponse.Fail(CartStatusCode.UserSessionNotFound, "User session hasn't been find");

            await _userStorage.RefreshSessionTimeAsync(request.UserSessionId);

            Product? product = await _dbContext.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == request.ProductId);
            if (product is null)
                return CartResponse.Fail(CartStatusCode.ProductNotFound, "Incorrect product Id");

            CartStateDto? cartState = await _cartStorage.GetStateAsync(userSession.UserId);

            if (cartState is null)
            {
                cartState = new CartStateDto(userSession.UserId);
                cartState.Products.Add(new CartItem(product.Id));

                bool isStateSet = await _cartStorage.SetStateAsync(cartState);
                if (!isStateSet)
                    return CartResponse.Fail(CartStatusCode.CartStateNotCreated, "The cart wasn't created");
            }
            else
            {
                bool isProductExist = cartState.Products.Exists(p => p.Id == product.Id);
                if (!isProductExist)
                {
                    cartState.Products.Add(new CartItem(product.Id));

                    bool isStateUpdated = await _cartStorage.UpdateStateAsync(cartState);
                    if (!isStateUpdated)
                        return CartResponse.Fail(CartStatusCode.CartStateNotUpdated, "The cart wasn't updated");
                }
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

        UserSessionDto? userSession = await _userStorage.GetSessionAsync(request.UserSessionId);
        if (userSession is null)
            return CartResponse.Fail(CartStatusCode.UserSessionNotFound, "User session hasn't been find");

        await _userStorage.RefreshSessionTimeAsync(request.UserSessionId);

        CartStateDto? cartState = await _cartStorage.GetStateAsync(userSession.UserId);
        if (cartState is null)
            return CartResponse.Fail(CartStatusCode.CartStateNotFound, "The cart wasn't found");

        await _cartStorage.RefreshStateTimeAsync(userSession.UserId);

        CartItem? cartItem = cartState.Products.FirstOrDefault(p => p.Id == request.ProductId);
        if (cartItem is null)
            return CartResponse.Fail(CartStatusCode.ProductNotFound, "The product in the cart wasn't found");

        if (cartItem.Quantity != request.Quantity)
        {
            cartItem.Quantity = request.Quantity;
            bool isStateUpdated = await _cartStorage.UpdateStateAsync(cartState);
            if (!isStateUpdated)
                return CartResponse.Fail(CartStatusCode.CartStateNotUpdated, "The cart wasn't updated");
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
            UserSessionDto? userSession = await _userStorage.GetSessionAsync(request.UserSessionId);
            if (userSession is null)
                return CartResponse.Fail(CartStatusCode.UserSessionNotFound, "User session wasn't found");

            await _userStorage.RefreshSessionTimeAsync(request.UserSessionId);

            CartStateDto? cartState = await _cartStorage.GetStateAsync(userSession.UserId);
            if (cartState is null)
                return CartResponse.Fail(CartStatusCode.CartStateNotFound, "The cart wasn't found");

            await _cartStorage.RefreshStateTimeAsync(userSession.UserId);

            CartItem? cartItem = cartState.Products.FirstOrDefault(p => p.Id == request.ProductId);
            if (cartItem is not null)
            {
                cartState.Products.Remove(cartItem);
                bool isStateUpdated = await _cartStorage.UpdateStateAsync(cartState);
                if (!isStateUpdated)
                    return CartResponse.Fail(CartStatusCode.CartStateNotUpdated, "The cart wasn't updated");
            }

            return CartResponse.Success("The product was deleted");
        }
        catch (Exception)
        {
            return CartResponse.Fail(CartStatusCode.UnknownError, "Internal server error");
        }
    }

    public async Task<CartResponse> DeleteCartAsync(DeleteCartRequest request)
    {
        ValidationResult validationResult = request.Validate();
        if (!validationResult.IsValid)
            return CartResponse.Fail(CartStatusCode.BadRequest, validationResult.Message);

        try
        {
            UserSessionDto? userSession = await _userStorage.GetSessionAsync(request.UserSessionId);
            if (userSession is null)
                return CartResponse.Fail(CartStatusCode.UserSessionNotFound, "User session wasn't found");

            await _userStorage.RefreshSessionTimeAsync(request.UserSessionId);

            bool isStateDeleted = await _cartStorage.DeleteStateAsync(userSession.UserId);
            if (!isStateDeleted)
                return CartResponse.Fail(CartStatusCode.CartStateNotDeleted, "The cart wasn't deleted");

            return CartResponse.Success("The cart was deleted");
        }
        catch (Exception)
        {
            return CartResponse.Fail(CartStatusCode.UnknownError, "Internal server error");
        }
    }
}
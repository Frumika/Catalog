using Backend.Application.DTO.Requests.Base;
using Backend.Application.DTO.Requests.Cart;
using Backend.Application.DTO.Responses;
using Backend.Application.Services.Interfaces;
using Backend.Application.StatusCodes;
using Backend.DataAccess.Postgres.Contexts;
using Backend.DataAccess.Storages.DTO;
using Backend.DataAccess.Storages.Interfaces;
using Backend.Domain.Models;
using Microsoft.EntityFrameworkCore;


namespace Backend.Application.Services;

public class CartService : ICartService
{
    private readonly MainDbContext _dbContext;
    private readonly IUserSessionStorage _userStorage;
    private readonly ICartStateStorage _cartStorage;

    public CartService(MainDbContext dbContext, ICartStateStorage cartStorage, IUserSessionStorage userStorage)
    {
        _dbContext = dbContext;
        _cartStorage = cartStorage;
        _userStorage = userStorage;
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

            Product? product = await _dbContext.Products
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == request.ProductId);
            if (product is null)
                return CartResponse.Fail(CartStatusCode.ProductNotFound, "Incorrect product Id");

            CartStateDto? cartState = await _cartStorage.GetStateAsync(userSession.UserId);

            if (cartState is null)
            {
                cartState = new CartStateDto(userSession.UserId);
                cartState.Products.Add(new ProductDto(product.Id));

                bool isStateSet = await _cartStorage.SetStateAsync(cartState);
                if (!isStateSet)
                    return CartResponse.Fail(CartStatusCode.CartStateNotCreated, "The cart wasn't created");
            }
            else
            {
                bool isProductExist = cartState.Products.Exists(p => p.Id == product.Id);
                if (!isProductExist)
                {
                    cartState.Products.Add(new ProductDto(product.Id));

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

        CartStateDto? cartState = await _cartStorage.GetStateAsync(userSession.UserId);
        if (cartState is null)
            return CartResponse.Fail(CartStatusCode.CartStateNotFound, "The cart wasn't found");

        ProductDto? productDto = cartState.Products.FirstOrDefault(p => p.Id == request.ProductId);
        if (productDto is null)
            return CartResponse.Fail(CartStatusCode.ProductNotFound, "The product in the cart wasn't found");

        if (productDto.Quantity != request.Quantity)
        {
            productDto.Quantity = request.Quantity;
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

            CartStateDto? cartState = await _cartStorage.GetStateAsync(userSession.UserId);
            if (cartState is null)
                return CartResponse.Fail(CartStatusCode.CartStateNotFound, "The cart wasn't found");

            ProductDto? productDto = cartState.Products.FirstOrDefault(p => p.Id == request.ProductId);
            if (productDto is not null)
            {
                cartState.Products.Remove(productDto);
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
}
using Backend.Application.DTO.Requests.Base;
using Backend.Application.DTO.Requests.Cart;
using Backend.Application.DTO.Responses;
using Backend.Application.StatusCodes;
using Backend.DataAccess.Postgres.Contexts;
using Backend.DataAccess.Storages.DTO;
using Backend.DataAccess.Storages.Interfaces;
using Backend.Domain.Models;
using Microsoft.EntityFrameworkCore;


namespace Backend.Application.Services;

public class CartService
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

    public async Task<CartResponse> HandleProductAsync(HandleProductRequest request)
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
                return CartResponse.Fail(CartStatusCode.ProductNotFound, "Incorrect Product Id");

            CartStateDto? cartState = await _cartStorage.GetStateAsync(userSession.Id);
            if (cartState is null)
            {
                cartState = new CartStateDto { UserId = userSession.Id };
                cartState.Products.Add(new ProductDto { Id = product.Id, Quantity = request.Quantity });

                bool isStateSet = await _cartStorage.SetStateAsync(cartState);
                if (!isStateSet)
                    return CartResponse.Fail(CartStatusCode.CartStateNotCreated, "The cart wasn't created");
            }
            else
            {
                bool isChanged = false;
                ProductDto? productDto = cartState.Products.FirstOrDefault(p => p.Id == request.ProductId);
                if (productDto is null)
                {
                    if (request.Quantity > 0)
                    {
                        productDto = new ProductDto
                        {
                            Id = product.Id,
                            Quantity = request.Quantity
                        };

                        cartState.Products.Add(productDto);
                        isChanged = true;
                    }
                }
                else
                {
                    if (request.Quantity == 0)
                    {
                        cartState.Products.Remove(productDto);
                        isChanged = true;
                    }
                    else if (productDto.Quantity != request.Quantity)
                    {
                        productDto.Quantity = request.Quantity;
                        isChanged = true;
                    }
                }

                if (isChanged)
                {
                    bool isStateUpdated = await _cartStorage.UpdateStateAsync(cartState);
                    if (!isStateUpdated)
                        return CartResponse.Fail(CartStatusCode.CartStateNotUpdated, "The cart wasn't updated");
                }
            }

            return CartResponse.Success("The product was handle");
        }
        catch (Exception)
        {
            return CartResponse.Fail(CartStatusCode.UnknownError, "Internal server error");
        }
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
                return CartResponse.Fail(CartStatusCode.UserSessionNotFound, "User session hasn't been find");
            
            CartStateDto? cartState = await _cartStorage.GetStateAsync(userSession.Id);
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
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

            if (request.CartStateId is null)
            {
                CartStateDto cartState = new()
                {
                    UserId = userSession.Id,
                    Products = new()
                };

                cartState.Products.Add(new ProductDto
                {
                    Id = product.Id,
                    Quantity = request.Quantity
                });

                await _cartStorage.SetStateAsync(cartState);
            }
            else
            {
                CartStateDto? cartState = await _cartStorage.GetStateAsync(userSession.Id);
                if (cartState is null)
                    return CartResponse.Fail(CartStatusCode.CartStateNotFound, "Cart State not found");

                ProductDto? productDto = cartState.Products?.FirstOrDefault(p => p.Id == request.ProductId);
                if (productDto is not null) productDto.Quantity = request.Quantity;
                else
                {
                    productDto = new()
                    {
                        Id = product.Id,
                        Quantity = request.Quantity
                    };
                }

                await _cartStorage.SetStateAsync(cartState);
            }

            return CartResponse.Success();
        }
        catch (Exception)
        {
            return CartResponse.Fail(CartStatusCode.UnknownError, "Internal server error");
        }
    }

    public async Task<CartResponse> RemoveProductAsync(int productId)
    {
        return CartResponse.Fail(CartStatusCode.UnknownError);
    }
}
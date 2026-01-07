using Backend.Application.DTO.Entities.Catalog;
using Backend.Application.DTO.Requests.Base;
using Backend.Application.DTO.Requests.Order;
using Backend.Application.DTO.Responses;
using Backend.Application.StatusCodes;
using Backend.DataAccess.Postgres.Contexts;
using Backend.DataAccess.Storages.DTO;
using Backend.DataAccess.Storages.Interfaces;
using Backend.Domain.Models;
using Microsoft.EntityFrameworkCore;


namespace Backend.Application.Services;

public class OrderService
{
    private readonly MainDbContext _dbContext;
    private readonly ICartStateStorage _cartStorage;
    private readonly IUserSessionStorage _userStorage;

    public OrderService(MainDbContext dbContext, ICartStateStorage cartStorage, IUserSessionStorage userStorage)
    {
        _dbContext = dbContext;
        _cartStorage = cartStorage;
        _userStorage = userStorage;
    }

    public async Task<OrderResponse> MakeOrder(MakeOrderRequest request)
    {
        ValidationResult validationResult = request.Validate();
        if (!validationResult.IsValid)
            return OrderResponse.Fail(OrderStatusCode.BadRequest, validationResult.Message);

        try
        {
            UserSessionDto? userSession = await _userStorage.GetSessionAsync(request.UserSessionId);
            if (userSession is null)
                return OrderResponse.Fail(OrderStatusCode.UserSessionNotFound, "The user session wasn't found");

            CartStateDto? cartState = await _cartStorage.GetStateAsync(userSession.UserId);
            if (cartState is null)
                return OrderResponse.Fail(OrderStatusCode.CartStateNotFound, "The cart wasn't found");

            List<Product> products = new();
            foreach (var productDto in cartState.Products)
            {
                Product? product = await _dbContext.Products
                    .AsNoTracking()
                    .FirstOrDefaultAsync(p => p.Id == productDto.Id);

                if (product is null)
                    return OrderResponse.Fail(OrderStatusCode.ProductNotFound, $"The product wasn't found");

                if (product.Count < productDto.Quantity)
                    return OrderResponse.Fail(
                        OrderStatusCode.IncorrectQuantity,
                        "The quantity of the product is insufficient"
                    );

                products.Add(product);
            }

            return OrderResponse.Success("Send the DTO");
        }
        catch (Exception)
        {
            return OrderResponse.Fail(OrderStatusCode.UnknownError, "Internal server error");
        }
    }

    public async Task<OrderResponse> PayOrder()
    {
        return OrderResponse.Success();
    }
}
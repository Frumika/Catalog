using Backend.Application.DTO.Requests.Base;
using Backend.Application.DTO.Requests.Order;
using Backend.Application.DTO.Responses;
using Backend.Application.StatusCodes;
using Backend.DataAccess.Postgres.Contexts;
using Backend.DataAccess.Storages;
using Backend.DataAccess.Storages.DTO;
using Backend.DataAccess.Storages.Interfaces;
using Backend.Domain.Models;
using Microsoft.EntityFrameworkCore;
using DaOrderStateDto = Backend.DataAccess.Storages.DTO.OrderStateDto;
using ApOrderStateDto = Backend.Application.DTO.Entities.Order.OrderStateDto;


namespace Backend.Application.Services;

public class OrderService
{
    private readonly MainDbContext _dbContext;
    private readonly IUserSessionStorage _userStorage;
    private readonly ICartStateStorage _cartStorage;
    private readonly OrderStateStorage _orderStorage;

    public OrderService(MainDbContext dbContext, ICartStateStorage cartStorage,
        IUserSessionStorage userStorage, OrderStateStorage orderStorage)
    {
        _dbContext = dbContext;
        _cartStorage = cartStorage;
        _userStorage = userStorage;
        _orderStorage = orderStorage;
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

            decimal finalPrice = 0m;
            List<OrderItem> orderItems = new();

            var productIds = cartState.Products.Select(p => p.Id);

            var products = await _dbContext.Products
                .AsNoTracking()
                .Where(p => productIds.Contains(p.Id))
                .ToDictionaryAsync(p => p.Id);
            
            foreach (var productDto in cartState.Products)
            {
                if (!products.TryGetValue(productDto.Id, out var product))
                    return OrderResponse.Fail(OrderStatusCode.ProductNotFound,
                        $"The product {productDto.Id} wasn't found"
                    );

                if (product.Count < productDto.Quantity)
                    return OrderResponse.Fail(OrderStatusCode.IncorrectQuantity,
                        "The quantity of the product is insufficient"
                    );

                var orderItem = new OrderItem(product, productDto.Quantity);
                finalPrice += orderItem.TotalPrice;

                orderItems.Add(orderItem);
            }

            DaOrderStateDto? orderState = await _orderStorage.GetStateAsync(userSession.UserId);
            if (orderState is null)
            {
                orderState = new DaOrderStateDto
                {
                    UserId = userSession.UserId,
                    OrderItems = orderItems,
                    FinalPrice = finalPrice
                };

                bool isStateSet = await _orderStorage.SetStateAsync(orderState);
                if (!isStateSet)
                    return OrderResponse.Fail(OrderStatusCode.CartStateNotCreated, "The order wasn't created");
            }
            else
            {
                orderState.UserId = userSession.UserId;
                orderState.OrderItems = orderItems.ToList();
                orderState.FinalPrice = finalPrice;

                bool isStateUpdated = await _orderStorage.UpdateStateAsync(orderState);
                if (!isStateUpdated)
                    return OrderResponse.Fail(OrderStatusCode.CartStateNotUpdated, "The order wasn't updated");
            }

            return OrderResponse.Success(new ApOrderStateDto(orderState));
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
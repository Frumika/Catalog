using Backend.Application.DTO.Requests.Base;
using Backend.Application.DTO.Requests.Order;
using Backend.Application.DTO.Responses;
using Backend.Application.StatusCodes;
using Backend.DataAccess.Postgres.Contexts;
using Backend.DataAccess.Storages;
using Backend.DataAccess.Storages.DTO;
using Backend.Domain.Models;
using Microsoft.EntityFrameworkCore;
using ResponseOrderStateDto = Backend.Application.DTO.Entities.Order.OrderStateDto;


namespace Backend.Application.Services;

public class OrderService
{
    private readonly MainDbContext _dbContext;
    private readonly UserSessionStorage _userStorage;
    private readonly CartStateStorage _cartStorage;
    private readonly OrderStateStorage _orderStorage;

    public OrderService(MainDbContext dbContext, CartStateStorage cartStorage,
        UserSessionStorage userStorage, OrderStateStorage orderStorage)
    {
        _dbContext = dbContext;
        _cartStorage = cartStorage;
        _userStorage = userStorage;
        _orderStorage = orderStorage;
    }

    public async Task<OrderResponse> MakeOrderAsync(MakeOrderRequest request)
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
                if (!products.TryGetValue(productDto.Id, out Product? product))
                    return OrderResponse.Fail(OrderStatusCode.ProductNotFound, $"The product wasn't found");

                if (product.Count < productDto.Quantity)
                    return OrderResponse.Fail(OrderStatusCode.IncorrectQuantity,
                        "The quantity of the product is insufficient"
                    );

                var orderItem = new OrderItem(product, productDto.Quantity);
                finalPrice += orderItem.TotalPrice;

                orderItems.Add(orderItem);
            }

            OrderStateDto? orderState = await _orderStorage.GetStateAsync(userSession.UserId);
            if (orderState is null)
            {
                orderState = new OrderStateDto
                {
                    UserId = userSession.UserId,
                    OrderItems = orderItems,
                    FinalPrice = finalPrice
                };

                bool isStateSet = await _orderStorage.SetStateAsync(orderState);
                if (!isStateSet)
                    return OrderResponse.Fail(OrderStatusCode.OrderStateNotCreated, "The order wasn't created");
            }
            else
            {
                orderState.UserId = userSession.UserId;
                orderState.OrderItems = orderItems.ToList();
                orderState.FinalPrice = finalPrice;

                bool isStateUpdated = await _orderStorage.UpdateStateAsync(orderState);
                if (!isStateUpdated)
                    return OrderResponse.Fail(OrderStatusCode.OrderStateNotUpdated, "The order wasn't updated");
            }

            return OrderResponse.Success(new ResponseOrderStateDto(orderState));
        }
        catch (Exception)
        {
            return OrderResponse.Fail(OrderStatusCode.UnknownError, "Internal server error");
        }
    }

    public async Task<OrderResponse> PayOrderAsync(PayOrderRequest request)
    {
        ValidationResult validationResult = request.Validate();
        if (!validationResult.IsValid)
            return OrderResponse.Fail(OrderStatusCode.BadRequest, validationResult.Message);

        await using var transaction = await _dbContext.Database.BeginTransactionAsync();

        try
        {
            UserSessionDto? userSession = await _userStorage.GetSessionAsync(request.UserSessionId);
            if (userSession is null)
                return OrderResponse.Fail(OrderStatusCode.UserSessionNotFound, "The user session wasn't found");

            OrderStateDto? orderState = await _orderStorage.GetStateAsync(userSession.UserId);
            if (orderState is null)
                return OrderResponse.Fail(OrderStatusCode.OrderStateNotFound, "The order wasn't found");

            var productIds = orderState.OrderItems.Select(o => o.ProductId);

            var products = await _dbContext.Products
                .Where(p => productIds.Contains(p.Id))
                .ToDictionaryAsync(p => p.Id);

            foreach (var orderItem in orderState.OrderItems)
            {
                if (!products.TryGetValue(orderItem.ProductId, out Product? product))
                    return OrderResponse.Fail(OrderStatusCode.ProductNotFound, $"The product wasn't found");

                if (product.Count < orderItem.Quantity)
                    return OrderResponse.Fail(
                        OrderStatusCode.IncorrectQuantity, "The quantity of the product is insufficient"
                    );
            }

            foreach (var orderItem in orderState.OrderItems)
            {
                Product product = products[orderItem.ProductId];
                product.Count -= orderItem.Quantity;
            }

            Order order = new()
            {
                UserId = orderState.UserId,
                PaymentTime = DateTime.UtcNow,
                FinalPrice = orderState.FinalPrice,
                OrderItems = orderState.OrderItems.ToList()
            };

            _dbContext.Orders.Add(order);

            await _dbContext.SaveChangesAsync();
            await transaction.CommitAsync();

            await _orderStorage.DeleteStateAsync(userSession.UserId);
            await _cartStorage.DeleteStateAsync(userSession.UserId);

            return OrderResponse.Success("The payment was successful");
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();

            return OrderResponse.Fail(OrderStatusCode.UnknownError, "Internal server error");
        }
    }
}
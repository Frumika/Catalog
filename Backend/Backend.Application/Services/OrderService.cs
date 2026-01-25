using Backend.Application.DTO.Entities.Order;
using Backend.Application.DTO.Requests.Base;
using Backend.Application.DTO.Requests.Order;
using Backend.Application.DTO.Responses;
using Backend.Application.StatusCodes;
using Backend.DataAccess.Postgres.Contexts;
using Backend.DataAccess.Storages;
using Backend.DataAccess.Storages.DTO;
using Backend.Domain.Models;
using Microsoft.EntityFrameworkCore;


namespace Backend.Application.Services;

public class OrderService
{
    private readonly MainDbContext _dbContext;
    private readonly UserSessionStorage _userStorage;
    private readonly CartStateStorage _cartStorage;

    public OrderService(MainDbContext dbContext, CartStateStorage cartStorage, UserSessionStorage userStorage)
    {
        _dbContext = dbContext;
        _cartStorage = cartStorage;
        _userStorage = userStorage;
    }

    public async Task<OrderResponse> MakeOrderAsync(MakeOrderRequest request)
    {
        ValidationResult validationResult = request.Validate();
        if (!validationResult.IsValid)
            return OrderResponse.Fail(OrderStatusCode.BadRequest, validationResult.Message);

        UserSessionDto? userSession;
        CartStateDto? cartState;
        Dictionary<int, Product> products;
        decimal totalPrice = 0m;

        try
        {
            userSession = await _userStorage.GetSessionAsync(request.UserSessionId);
            if (userSession is null)
                return OrderResponse.Fail(OrderStatusCode.UserSessionNotFound, "The user session wasn't found");

            await _userStorage.RefreshSessionTimeAsync(request.UserSessionId);

            cartState = await _cartStorage.GetStateAsync(userSession.UserId);
            if (cartState is null)
                return OrderResponse.Fail(OrderStatusCode.CartStateNotFound, "The cart wasn't found");

            await _cartStorage.RefreshStateTimeAsync(userSession.UserId);
        }
        catch (Exception)
        {
            return OrderResponse.Fail(OrderStatusCode.UnknownError, "Internal server error");
        }

        await using var transaction = await _dbContext.Database.BeginTransactionAsync();
        try
        {
            List<OrderedProduct> orderedProducts = new();
            List<OrderItemDto> orderItems = new();

            var productIds = cartState.Products.Select(p => p.Id);
            products = await _dbContext.Products
                .Where(p => productIds.Contains(p.Id))
                .ToDictionaryAsync(p => p.Id);

            foreach (var cartItem in cartState.Products)
            {
                if (!products.TryGetValue(cartItem.Id, out Product? product))
                    return OrderResponse.Fail(OrderStatusCode.ProductNotFound, $"The product wasn't found");

                if (product.Count < cartItem.Quantity)
                    return OrderResponse.Fail(OrderStatusCode.IncorrectQuantity,
                        "The quantity of the product is insufficient"
                    );

                product.Count -= cartItem.Quantity;
                totalPrice += product.Price * cartItem.Quantity;

                OrderedProduct orderedProduct = new(product, cartItem.Quantity);
                OrderItemDto orderItemDto = new(product, cartItem.Quantity);

                orderedProducts.Add(orderedProduct);
                orderItems.Add(orderItemDto);
            }

            DateTime createdAt = DateTime.UtcNow;
            Order order = new()
            {
                Status = OrderStatus.Pending,
                TotalPrice = totalPrice,
                CreatedAt = createdAt,
                DeletionTime = createdAt + TimeSpan.FromMinutes(5),
                UserId = userSession.UserId,
                OrderedProducts = orderedProducts
            };

            await _dbContext.Orders.AddAsync(order);
            await _dbContext.SaveChangesAsync();
            await transaction.CommitAsync();

            return OrderResponse.Success(new OrderDto { OrderItems = orderItems, TotalPrice = totalPrice });
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            return OrderResponse.Fail(OrderStatusCode.UnknownError, "Internal server error");
        }
    }

    public async Task<OrderResponse> PayOrderAsync(PayOrderRequest request)
    {
        ValidationResult validationResult = request.Validate();
        if (!validationResult.IsValid)
            return OrderResponse.Fail(OrderStatusCode.BadRequest, validationResult.Message);

        UserSessionDto? userSession;
        try
        {
            userSession = await _userStorage.GetSessionAsync(request.UserSessionId);
            if (userSession is null)
                return OrderResponse.Fail(OrderStatusCode.UserSessionNotFound, "The user session wasn't found");

            await _userStorage.RefreshSessionTimeAsync(request.UserSessionId);
        }
        catch (Exception)
        {
            return OrderResponse.Fail(OrderStatusCode.UnknownError, "Internal server error");
        }


        await using var transaction = await _dbContext.Database.BeginTransactionAsync();
        try
        {
            var pendingOrders = await _dbContext.Orders
                .Where(o =>
                    o.UserId == userSession.UserId &&
                    o.Status == OrderStatus.Pending)
                .ToListAsync();

            if (!pendingOrders.Any())
                return OrderResponse.Fail(OrderStatusCode.OrderNotFound, "There are no pending orders to pay");

            DateTime paidAt = DateTime.UtcNow;
            foreach (var order in pendingOrders)
            {
                order.Status = OrderStatus.Paid;
                order.PaidAt = paidAt;
            }

            await _dbContext.SaveChangesAsync();
            await transaction.CommitAsync();

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
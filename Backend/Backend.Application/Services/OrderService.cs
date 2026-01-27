using Backend.Application.DTO.Entities.Order;
using Backend.Application.DTO.Requests.Base;
using Backend.Application.DTO.Requests.Order;
using Backend.Application.DTO.Responses;
using Backend.Application.Exceptions;
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
    private readonly CartStateStorage _cartStorage;
    private readonly OrderIndexStorage _orderStorage;
    private readonly UserSessionStorage _userStorage;

    public OrderService(MainDbContext dbContext, CartStateStorage cartStorage,
        OrderIndexStorage orderStorage, UserSessionStorage userStorage)
    {
        _dbContext = dbContext;
        _cartStorage = cartStorage;
        _orderStorage = orderStorage;
        _userStorage = userStorage;
    }

    public async Task<OrderResponse> MakeOrderAsync(MakeOrderRequest request)
    {
        ValidationResult validationResult = request.Validate();
        if (!validationResult.IsValid)
            return OrderResponse.Fail(OrderStatusCode.BadRequest, validationResult.Message);

        UserSessionDto? userSession;
        CartStateDto? cartState;
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

            bool isOrderExists = await _orderStorage.IsOrderExists(request.UserSessionId);
            if (isOrderExists)
            {
                OrderResponse response = await GetPendingOrderAsync(request.UserSessionId);
                return response;
            }
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
            Dictionary<int, Product> products = await _dbContext.Products
                .Where(p => productIds.Contains(p.Id))
                .ToDictionaryAsync(p => p.Id);

            foreach (var cartItem in cartState.Products)
            {
                if (!products.TryGetValue(cartItem.Id, out Product? product))
                    throw new OrderException(
                        OrderStatusCode.ProductNotFound,
                        "The product wasn't found"
                    );

                if (product.Count < cartItem.Quantity)
                    throw new OrderException(
                        OrderStatusCode.IncorrectQuantity,
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

            await _orderStorage.SetStateAsync(request.UserSessionId, order.Id);

            return OrderResponse.Success(new OrderDto { OrderItems = orderItems, TotalPrice = totalPrice });
        }
        catch (OrderException orderException)
        {
            await transaction.RollbackAsync();
            return OrderResponse.Fail(orderException.StatusCode, orderException.Message);
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            return OrderResponse.Fail(OrderStatusCode.UnknownError, "Internal server error");
        }
    }


    private async Task<OrderResponse> GetPendingOrderAsync(string userSessionId)
    {
        try
        {
            int? orderId = await _orderStorage.GetOrderIdAsync(userSessionId);
            if (orderId is null)
                return OrderResponse.Fail(OrderStatusCode.OrderNotFound, "The order wasn't found");

            var orderItems = await _dbContext.OrderedProducts
                .AsNoTracking()
                .Where(op => op.OrderId == orderId)
                .Select(op => new OrderItemDto
                {
                    Id = op.ProductId,
                    Name = op.Product.Name,
                    Quantity = op.Quantity,
                    Price = op.ProductPrice
                })
                .ToListAsync();

            decimal totalPrice = 0m;
            foreach (var orderItem in orderItems)
                totalPrice += orderItem.TotalPrice;

            return OrderResponse.Success(new OrderDto { OrderItems = orderItems, TotalPrice = totalPrice });
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

        int? orderId;
        UserSessionDto? userSession;
        try
        {
            userSession = await _userStorage.GetSessionAsync(request.UserSessionId);
            if (userSession is null)
                return OrderResponse.Fail(OrderStatusCode.UserSessionNotFound, "The user session wasn't found");

            await _userStorage.RefreshSessionTimeAsync(request.UserSessionId);

            orderId = await _orderStorage.GetOrderIdAsync(request.UserSessionId);
            if (orderId is null)
            {
                await _orderStorage.DeleteStateAsync(request.UserSessionId);
                return OrderResponse.Fail(OrderStatusCode.OrderNotFound, "Order wasn't found");
            }
        }
        catch (Exception)
        {
            return OrderResponse.Fail(OrderStatusCode.UnknownError, "Internal server error");
        }

        await using var transaction = await _dbContext.Database.BeginTransactionAsync();
        try
        {
            Order? pendingOrder = await _dbContext.Orders.FirstOrDefaultAsync(o =>
                o.Id == orderId &&
                o.UserId == userSession.UserId &&
                o.Status == OrderStatus.Pending);

            if (pendingOrder is null)
                throw new OrderException(OrderStatusCode.OrderNotFound, "The order doesn't exist");

            pendingOrder.Status = OrderStatus.Paid;
            pendingOrder.PaidAt = DateTime.UtcNow;

            await _dbContext.SaveChangesAsync();
            await transaction.CommitAsync();

            await _cartStorage.DeleteStateAsync(userSession.UserId);
            await _orderStorage.DeleteStateAsync(request.UserSessionId);

            return OrderResponse.Success("The payment was successful");
        }
        catch (OrderException orderException)
        {
            await transaction.RollbackAsync();
            return OrderResponse.Fail(orderException.StatusCode, orderException.Message);
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            return OrderResponse.Fail(OrderStatusCode.UnknownError, "Internal server error");
        }
    }
}
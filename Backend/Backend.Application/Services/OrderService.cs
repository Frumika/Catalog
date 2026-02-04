using Backend.Application.DTO.Entities.Order;
using Backend.Application.DTO.Requests.Base;
using Backend.Application.DTO.Requests.Order;
using Backend.Application.DTO.Responses;
using Backend.Application.Exceptions;
using Backend.Application.StatusCodes;
using Backend.DataAccess.Postgres.Contexts;
using Backend.DataAccess.Storages;
using Backend.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;


namespace Backend.Application.Services;

public class OrderService
{
    private readonly MainDbContext _dbContext;

    private readonly OrderIndexStorage _orderStorage;
    private readonly UserSessionStorage _userStorage;

    public OrderService(MainDbContext dbContext, OrderIndexStorage orderStorage, UserSessionStorage userStorage)
    {
        _dbContext = dbContext;
        _orderStorage = orderStorage;
        _userStorage = userStorage;
    }

    public async Task<OrderResponse> MakeOrderAsync(MakeOrderRequest request)
    {
        ValidationResult validationResult = request.Validate();
        if (!validationResult.IsValid)
            return OrderResponse.Fail(OrderStatusCode.BadRequest, validationResult.Message);

        int? userId;
        try
        {
            userId = await _userStorage.GetUserIdAsync(request.UserSessionId);
            if (userId is null)
                return OrderResponse.Fail(OrderStatusCode.UserSessionNotFound, "The user session wasn't found");

            await _userStorage.RefreshSessionTimeAsync(request.UserSessionId);

            bool isOrderExists = await _orderStorage.IsOrderExists(request.UserSessionId);
            if (isOrderExists) await CancelOrderAsync(request.UserSessionId);
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

            int? cartId = await _dbContext.Carts
                .AsNoTracking()
                .Where(c => c.UserId == userId)
                .Select(c => (int?)c.Id)
                .FirstOrDefaultAsync();
            if (cartId is null)
                throw new OrderException(OrderStatusCode.CartNotFound, "The cart wasn't found");

            var cartItems = await _dbContext.CartItems
                .Where(ci => ci.CartId == cartId)
                .Include(ci => ci.Product)
                .ToListAsync();
            if (cartItems.Count == 0)
                throw new OrderException(OrderStatusCode.CartNotFound, "The cart is empty");

            decimal totalPrice = 0m;
            foreach (var cartItem in cartItems)
            {
                Product product = cartItem.Product;

                if (product.Quantity < cartItem.Quantity)
                    throw new OrderException(
                        OrderStatusCode.IncorrectQuantity,
                        "The quantity of the product is insufficient"
                    );

                product.Quantity -= cartItem.Quantity;
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
                UserId = userId.Value,
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


    public async Task<OrderResponse> PayOrderAsync(PayOrderRequest request)
    {
        ValidationResult validationResult = request.Validate();
        if (!validationResult.IsValid)
            return OrderResponse.Fail(OrderStatusCode.BadRequest, validationResult.Message);

        int? orderId;
        int? userId;
        try
        {
            userId = await _userStorage.GetUserIdAsync(request.UserSessionId);
            if (userId is null)
                return OrderResponse.Fail(OrderStatusCode.UserSessionNotFound, "The user session wasn't found");

            await _userStorage.RefreshSessionTimeAsync(request.UserSessionId);

            orderId = await _orderStorage.GetOrderIdAsync(request.UserSessionId);
            if (orderId is null)
                return OrderResponse.Fail(OrderStatusCode.OrderNotFound, "Order wasn't found");
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
                o.UserId == userId &&
                o.Status == OrderStatus.Pending);

            if (pendingOrder is null)
                throw new OrderException(OrderStatusCode.OrderNotFound, "The order doesn't exist");

            pendingOrder.Status = OrderStatus.Paid;
            pendingOrder.PaidAt = DateTime.UtcNow;

            await _dbContext.SaveChangesAsync();
            await transaction.CommitAsync();

            int? cartId = await _dbContext.Carts
                .AsNoTracking()
                .Where(c => c.UserId == userId)
                .Select(c => (int?)c.Id)
                .FirstOrDefaultAsync();

            await _dbContext.CartItems
                .Where(ci => ci.Cart.Id == cartId)
                .ExecuteDeleteAsync();

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


    public async Task<OrderResponse> CancelOrderAsync(CancelOrderRequest request)
    {
        ValidationResult validationResult = request.Validate();
        if (!validationResult.IsValid)
            return OrderResponse.Fail(OrderStatusCode.BadRequest, validationResult.Message);

        try
        {
            int? userId = await _userStorage.GetUserIdAsync(request.UserSessionId);
            if (userId is null)
                throw new OrderException(OrderStatusCode.UserSessionNotFound, "User session wasn't found");
            
            await _userStorage.RefreshSessionTimeAsync(request.UserSessionId);

            OrderResponse response = await CancelOrderAsync(request.UserSessionId);
            return response;
        }
        catch (OrderException orderException)
        {
            return OrderResponse.Fail(orderException.StatusCode, orderException.Message);
        }
        catch (Exception)
        {
            return OrderResponse.Fail(OrderStatusCode.UnknownError, "Internal server error");
        }
    }


    private async Task<OrderResponse> CancelOrderAsync(string userSessionId)
    {
        int? orderId;
        try
        {
            orderId = await _orderStorage.GetOrderIdAsync(userSessionId);
            if (orderId is null)
                return OrderResponse.Success("The order doesn't exist");
        }
        catch (OrderException orderException)
        {
            return OrderResponse.Fail(orderException.StatusCode, orderException.Message);
        }
        catch (Exception)
        {
            return OrderResponse.Fail(OrderStatusCode.UnknownError, "Internal server error");
        }

        await using IDbContextTransaction transaction = await _dbContext.Database.BeginTransactionAsync();
        try
        {
            Order? order = await _dbContext.Orders
                .Include(o => o.OrderedProducts)
                .ThenInclude(op => op.Product)
                .FirstOrDefaultAsync(o => o.Id == orderId);

            if (order is null)
                return OrderResponse.Success("The order doesn't exist");

            if (order.Status != OrderStatus.Pending)
                throw new OrderException(OrderStatusCode.InvalidOrderStatus, "The order has already been paid");


            foreach (OrderedProduct orderedProduct in order.OrderedProducts)
            {
                orderedProduct.Product.Quantity += orderedProduct.Quantity;
            }

            _dbContext.Orders.Remove(order);

            await _dbContext.SaveChangesAsync();
            await transaction.CommitAsync();

            await _orderStorage.DeleteStateAsync(userSessionId);

            return OrderResponse.Success("The order was canceled");
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
}
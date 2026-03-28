using Backend.Application.DTO.Entities.Order;
using Backend.Application.DTO.Requests.Base;
using Backend.Application.DTO.Requests.Order;
using Backend.Application.DTO.Responses;
using Backend.Application.Errors;
using Backend.Application.Exceptions;
using Backend.DataAccess.Postgres.Contexts;
using Backend.Domain.Models;
using Backend.Domain.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;


namespace Backend.Application.Services;

public class OrderService
{
    private readonly OrderSettings _settings;
    private readonly MainDbContext _dbContext;

    public OrderService(OrderSettings settings, MainDbContext dbContext)
    {
        _settings = settings;
        _dbContext = dbContext;
    }

    public async Task<Response> MakeOrderAsync(MakeOrderRequest request)
    {
        ValidationResult result = request.Validate();
        if (!result.IsValid)
            return Response.Fail(new BadRequest(), result.Message);

        UserSession? userSession;
        try
        {
            userSession = await _dbContext.UserSessions
                .Where(us => us.UId == request.UserSessionId)
                .FirstOrDefaultAsync();

            if (userSession is null)
                return Response.Fail(new UserNotFound(), "The user session wasn't found");

            if (userSession.OrderId is not null) await CancelOrderAsync(userSession.OrderId.Value);
        }
        catch (Exception)
        {
            return Response.Fail(new UnknownError(), "Internal server error");
        }

        await using var transaction = await _dbContext.Database.BeginTransactionAsync();
        try
        {
            List<OrderedProduct> orderedProducts = new();
            List<OrderItemDto> orderItems = new();

            int cartId = await _dbContext.Carts
                .AsNoTracking()
                .Where(c => c.UserId == userSession.UserId)
                .Select(c => c.Id)
                .FirstAsync();

            List<CartItem> cartItems = await _dbContext.CartItems
                .Where(ci => ci.CartId == cartId)
                .OrderBy(ci => ci.AddedAt)
                .Include(ci => ci.Product)
                .ToListAsync();
            if (cartItems.Count == 0)
                throw new ServiceException(new CartNotFound(), "The cart is empty");

            decimal totalPrice = 0m;
            foreach (CartItem cartItem in cartItems)
            {
                Product product = cartItem.Product;

                if (product.Quantity < cartItem.Quantity)
                    throw new ServiceException(
                        new IncorrectQuantity(),
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
                DeletionTime = createdAt + _settings.Lifetime,
                UserId = userSession.UserId,
                OrderedProducts = orderedProducts
            };

            userSession.PendingOrder = order;

            await _dbContext.Orders.AddAsync(order);
            await _dbContext.SaveChangesAsync();
            await transaction.CommitAsync();

            return Response.Success(new OrderDto { OrderItems = orderItems, TotalPrice = totalPrice });
        }
        catch (ServiceException e)
        {
            await transaction.RollbackAsync();
            return Response.Fail(e.Error, e.Message);
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            return Response.Fail(new UnknownError(), "Internal server error");
        }
    }


    public async Task<Response> PayOrderAsync(PayOrderRequest request)
    {
        ValidationResult result = request.Validate();
        if (!result.IsValid)
            return Response.Fail(new BadRequest(), result.Message);

        UserSession? userSession;
        try
        {
            userSession = await _dbContext.UserSessions
                .Where(us => us.UId == request.UserSessionId)
                .Include(us => us.PendingOrder)
                .FirstOrDefaultAsync();
            if (userSession is null)
                return Response.Fail(new UserSessionNotFound(), "The user session wasn't found");

            if (userSession.PendingOrder is null)
                return Response.Fail(new OrderNotFound(), "Order wasn't found");
        }
        catch (Exception)
        {
            return Response.Fail(new UnknownError(), "Internal server error");
        }

        await using var transaction = await _dbContext.Database.BeginTransactionAsync();
        try
        {
            Order pendingOrder = userSession.PendingOrder;

            pendingOrder.Status = OrderStatus.Paid;
            pendingOrder.PaidAt = DateTime.UtcNow;

            userSession.PendingOrder = null;

            await _dbContext.SaveChangesAsync();
            await transaction.CommitAsync();

            int? cartId = await _dbContext.Carts
                .AsNoTracking()
                .Where(c => c.UserId == userSession.UserId)
                .Select(c => (int?)c.Id)
                .FirstOrDefaultAsync();

            await _dbContext.CartItems
                .Where(ci => ci.Cart.Id == cartId)
                .ExecuteDeleteAsync();

            return Response.Success("The payment was successful");
        }
        catch (ServiceException e)
        {
            await transaction.RollbackAsync();
            return Response.Fail(e.Error, e.Message);
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            return Response.Fail(new UnknownError(), "Internal server error");
        }
    }


    public async Task<Response> CancelOrderAsync(CancelOrderRequest request)
    {
        ValidationResult result = request.Validate();
        if (!result.IsValid)
            return Response.Fail(new BadRequest(), result.Message);

        try
        {
            UserSession? userSession = await _dbContext.UserSessions
                .AsNoTracking()
                .Where(us => us.UId == request.UserSessionId)
                .FirstOrDefaultAsync();
            if (userSession is null)
                return Response.Fail(new UserSessionNotFound(), "User session wasn't found");

            return userSession.OrderId is null
                ? Response.Success("The order was canceled")
                : await CancelOrderAsync(userSession.OrderId.Value);
        }
        catch (Exception)
        {
            return Response.Fail(new UnknownError(), "Internal server error");
        }
    }


    private async Task<Response> CancelOrderAsync(int orderId)
    {
        await using IDbContextTransaction transaction = await _dbContext.Database.BeginTransactionAsync();
        try
        {
            Order? order = await _dbContext.Orders
                .Include(o => o.OrderedProducts)
                .ThenInclude(op => op.Product)
                .FirstOrDefaultAsync(o => o.Id == orderId);

            if (order is null)
                return Response.Success("The order doesn't exist");

            if (order.Status != OrderStatus.Pending)
                throw new ServiceException(new InvalidOrderStatus(), "The order has already been paid");


            foreach (OrderedProduct orderedProduct in order.OrderedProducts)
            {
                orderedProduct.Product.Quantity += orderedProduct.Quantity;
            }

            _dbContext.Orders.Remove(order);

            await _dbContext.SaveChangesAsync();
            await transaction.CommitAsync();

            return Response.Success("The order was canceled");
        }
        catch (ServiceException e)
        {
            await transaction.RollbackAsync();
            return Response.Fail(e.Error, e.Message);
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            return Response.Fail(new UnknownError(), "Internal server error");
        }
    }


    private async Task<Response> GetPendingOrderAsync(string userSessionId)
    {
        try
        {
            int? orderId = await _dbContext.UserSessions
                .Where(us => us.UId == userSessionId)
                .Select(us => us.OrderId)
                .FirstOrDefaultAsync();
            if (orderId is null)
                return Response.Fail(new OrderNotFound(), "The order wasn't found");

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

            return Response.Success(new OrderDto { OrderItems = orderItems, TotalPrice = totalPrice });
        }
        catch (Exception)
        {
            return Response.Fail(new UnknownError(), "Internal server error");
        }
    }
}
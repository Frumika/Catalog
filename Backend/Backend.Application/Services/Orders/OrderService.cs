using Backend.Application.Common;
using Backend.Application.Common.Statuses;
using Backend.Application.DataAccess.Contexts;
using Backend.Application.Services.Orders.Dtos;
using Backend.Application.Services.Orders.Requests;
using Backend.Domain.Models;
using Backend.Domain.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;


namespace Backend.Application.Services.Orders;

public class OrderService
{
    private readonly OrderSettings _settings;
    private readonly MainDbContext _dbContext;

    public OrderService(OrderSettings settings, MainDbContext dbContext)
    {
        _settings = settings;
        _dbContext = dbContext;
    }

    public async Task<Response> MakeOrderAsync(int userId, MakeOrderRequest request)
    {
        if (request.ProductIds.Count == 0)
            return Response.Fail(new BadRequest(), "No products specified for ordering");

        List<int> requestedProductIds = request.ProductIds.Distinct().ToList();

        await using var transaction = await _dbContext.Database.BeginTransactionAsync();
        try
        {
            int cartId = await _dbContext.Carts
                .AsNoTracking()
                .Where(c => c.UserId == userId)
                .Select(c => c.Id)
                .FirstOrDefaultAsync();

            List<CartPosition> cartItems = await _dbContext.CartPositions
                .Where(ci => ci.CartId == cartId && requestedProductIds.Contains(ci.ProductId))
                .OrderBy(ci => ci.AddedAt)
                .Include(ci => ci.Product)
                .ThenInclude(p => p.ProductImages)
                .ToListAsync();

            if (cartItems.Count < requestedProductIds.Count)
                return Response.Fail(new BadRequest(), "One or more requested products are not in the cart");


            List<OrderPosition> orderedPositions = new();
            List<OrderPositionDto> orderPositionDtos = new();

            foreach (CartPosition cartItem in cartItems)
            {
                Product product = cartItem.Product;

                if (product.Quantity < cartItem.Quantity)
                    throw new ServiceException(
                        new IncorrectQuantity(),
                        "The quantity of the product is insufficient"
                    );

                product.Quantity -= cartItem.Quantity;

                OrderPosition orderPosition = new()
                {
                    ProductId = product.Id,
                    Quantity = cartItem.Quantity,
                    DiscountPercent = product.DiscountPercent,
                    Price = product.Price,
                };

                OrderPositionDto orderPositionDto = new()
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    Quantity = cartItem.Quantity,
                    Price = (int)Math.Round(product.Price, 0),
                    DiscountPercent = product.DiscountPercent,
                    DiscountedPrice = (int)Math.Round(product.Price * (100 - product.DiscountPercent) / 100m, 0),
                    ImageUrl = product.ProductImages
                        .OrderBy(pi => pi.Position)
                        .Select(pi => pi.Path)
                        .FirstOrDefault()
                };

                orderedPositions.Add(orderPosition);
                orderPositionDtos.Add(orderPositionDto);
            }

            DateTime createdAt = DateTime.UtcNow;
            Order order = new()
            {
                Status = OrderStatus.Pending,
                CreatedAt = createdAt,
                DeletionTime = createdAt + _settings.Lifetime,
                UserId = userId,
                OrderedProducts = orderedPositions
            };

            await _dbContext.Orders.AddAsync(order);
            await _dbContext.SaveChangesAsync();
            await transaction.CommitAsync();

            return Response.Success(orderPositionDtos);
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


    public async Task<Response> PayOrderAsync(int userId, int orderId)
    {
        await using var transaction = await _dbContext.Database.BeginTransactionAsync();
        try
        {
            Order? pendingOrder = await _dbContext.Orders
                .Where(o => o.Id == orderId && o.UserId == userId && o.Status == OrderStatus.Pending)
                .FirstOrDefaultAsync();

            if (pendingOrder is null)
                return Response.Fail(new OrderNotFound(), "Pending Order doesn't exist");

            pendingOrder.Status = OrderStatus.Paid;
            pendingOrder.PaidAt = DateTime.UtcNow;

            await _dbContext.SaveChangesAsync();
            await transaction.CommitAsync();

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


    public async Task<Response> CancelOrderAsync(int userId, int orderId)
    {
        await using IDbContextTransaction transaction = await _dbContext.Database.BeginTransactionAsync();
        try
        {
            Order? order = await _dbContext.Orders
                .Where(o => o.Id == orderId && o.UserId == userId && o.Status == OrderStatus.Pending)
                .Include(o => o.OrderedProducts)
                .ThenInclude(op => op.Product)
                .FirstOrDefaultAsync();

            if (order is null)
                return Response.Success("The order doesn't exist");

            if (order.Status != OrderStatus.Pending)
                throw new ServiceException(new InvalidOrderStatus(), "The order has already been paid");


            foreach (OrderPosition orderedProduct in order.OrderedProducts)
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


    private async Task<Response> GetPendingOrderAsync(string refreshToken)
    {
        // try
        // {
        //     int? orderId = await _dbContext.RefreshTokens
        //         .Where(us => us.Token == refreshToken)
        //         .Select(us => us.OrderId)
        //         .FirstOrDefaultAsync();
        //     if (orderId is null)
        //         return Response.Fail(new OrderNotFound(), "The order wasn't found");
        //
        //     var orderItems = await _dbContext.OrderedProducts
        //         .AsNoTracking()
        //         .Where(op => op.OrderId == orderId)
        //         .Select(op => new OrderItemDto
        //         {
        //             Id = op.ProductId,
        //             Name = op.Product.Name,
        //             Quantity = op.Quantity,
        //             Price = op.ProductPrice
        //         })
        //         .ToListAsync();
        //
        //     decimal totalPrice = 0m;
        //     foreach (var orderItem in orderItems)
        //         totalPrice += orderItem.TotalPrice;
        //
        //     return Response.Success(new OrderDto { OrderItems = orderItems, TotalPrice = totalPrice });
        // }
        // catch (Exception)
        // {
        //     return Response.Fail(new UnknownError(), "Internal server error");
        // }

        return Response.Success();
    }
}
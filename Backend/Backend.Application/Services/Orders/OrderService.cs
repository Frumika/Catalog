using Backend.Application.Common;
using Backend.Application.Common.Statuses;
using Backend.Application.DataAccess.Contexts;
using Backend.Application.Services.Orders.Dtos;
using Backend.Application.Services.Orders.Requests;
using Backend.Domain.Interfaces;
using Backend.Domain.Models;
using Backend.Domain.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;


namespace Backend.Application.Services.Orders;

public class OrderService
{
    private readonly OrderSettings _settings;
    private readonly MainDbContext _dbContext;
    private readonly IPaymentService _paymentService;

    public OrderService(OrderSettings settings, MainDbContext dbContext, IPaymentService paymentService)
    {
        _settings = settings;
        _dbContext = dbContext;
        _paymentService = paymentService;
    }

    public async Task<Response> MakeOrderAsync(int userId, MakeOrderRequest request)
    {
        if (request.ProductIds.Count == 0)
            return Response.Fail(new BadRequest(), "No products specified for ordering");

        List<int> requestedProductIds = request.ProductIds.Distinct().ToList();

        await using var transaction = await _dbContext.Database.BeginTransactionAsync();
        try
        {
            int pendingOrdersCount = await _dbContext.Orders
                .CountAsync(o => o.UserId == userId && o.Status == OrderStatus.Pending);

            if (pendingOrdersCount >= 3)
                return Response.Fail(new LimitExceeded(), "You cannot have more than 3 pending orders");


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

            bool pickupPointExists = await _dbContext.PickupPoints
                .AnyAsync(pp => pp.Id == request.PickupPointId);
            if (!pickupPointExists)
                return Response.Fail(new PickupPointNotFound(), "PickupPoint doesn't exist");

            DateTime createdAt = DateTime.UtcNow;
            DateTime deliveryDate = createdAt + TimeSpan.FromDays(14);

            Order order = new()
            {
                Status = OrderStatus.Pending,
                CreatedAt = createdAt,
                DeliveryDate = deliveryDate,
                DeletionTime = createdAt + _settings.Lifetime,
                UserId = userId,
                PickupPointId = request.PickupPointId,
                OrderedProducts = orderedPositions
            };

            await _dbContext.Orders.AddAsync(order);
            await _dbContext.SaveChangesAsync();
            await transaction.CommitAsync();

            return Response.Success(
                new ExtendedOrderDto
                {
                    OrderId = order.Id,
                    Status = order.Status.ToString(),
                    CreatedAt = order.CreatedAt,
                    DeliveryDate = order.DeliveryDate,
                    OrderPositions = orderPositionDtos
                }
            );
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
                .Include(o => o.OrderedProducts)
                .Where(o => o.Id == orderId && o.UserId == userId && o.Status == OrderStatus.Pending)
                .FirstOrDefaultAsync();

            if (pendingOrder is null)
                throw new ServiceException(new OrderNotFound(), "Order doesn't exist");
            
            int cartId = await _dbContext.Carts
                .Where(c => c.UserId == userId)
                .Select(c => c.Id)
                .FirstOrDefaultAsync();

            if (pendingOrder.OrderedProducts.Any())
            {
                var orderedProductIds = pendingOrder.OrderedProducts.Select(op => op.ProductId).ToList();

                List<CartPosition> cartPositions = await _dbContext.CartPositions
                    .Where(cp => cp.CartId == cartId && orderedProductIds.Contains(cp.ProductId))
                    .ToListAsync();

                _dbContext.CartPositions.RemoveRange(cartPositions);
            }
            
            pendingOrder.Status = OrderStatus.Paid;
            pendingOrder.PaidAt = DateTime.UtcNow;
            
            await _paymentService.Pay(pendingOrder);

            await _dbContext.SaveChangesAsync();
            await transaction.CommitAsync();

            return Response.Success(
                new OrderDto
                {
                    OrderId = orderId,
                    CreatedAt = pendingOrder.CreatedAt,
                    PaidAt = pendingOrder.PaidAt,
                    DeliveryDate = pendingOrder.DeliveryDate,
                    Status = pendingOrder.Status.ToString(),
                }
            );
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


    public async Task<Response> GetPendingOrdersAsync(int userId)
    {
        try
        {
            List<OrderDto> orders = await _dbContext.Orders
                .AsNoTracking()
                .Where(o => o.UserId == userId && o.Status == OrderStatus.Pending)
                .Select(o => new OrderDto
                {
                    OrderId = o.Id,
                    Status = o.Status.ToString(),
                    CreatedAt = o.CreatedAt,
                })
                .ToListAsync();

            return Response.Success(orders);
        }
        catch (Exception)
        {
            return Response.Fail(new UnknownError(), "Internal server error");
        }
    }


    public async Task<Response> GetPendingOrderAsync(int userId, int orderId)
    {
        try
        {
            Order? order = await _dbContext.Orders
                .AsNoTracking()
                .Where(o => o.Id == orderId && o.UserId == userId && o.Status == OrderStatus.Pending)
                .FirstOrDefaultAsync();

            if (order is null)
                return Response.Fail(new OrderNotFound(), "The order doesn't exist");

            List<OrderPositionDto> orderPositionDtos = await _dbContext.OrderedProducts
                .AsNoTracking()
                .Where(op => op.OrderId == orderId)
                .Select(op => new OrderPositionDto
                {
                    ProductId = op.ProductId,
                    ProductName = op.Product.Name,
                    Quantity = op.Quantity,
                    Price = (int)Math.Round(op.Price, 0),
                    DiscountPercent = op.DiscountPercent,
                    DiscountedPrice = (int)Math.Round(op.Price * (100 - op.DiscountPercent) / 100m, 0),
                    ImageUrl = op.Product.ProductImages
                        .OrderBy(pi => pi.Position)
                        .Select(pi => pi.Path)
                        .FirstOrDefault()
                })
                .ToListAsync();

            return Response.Success(
                new ExtendedOrderDto
                {
                    OrderId = order.Id,
                    Status = order.Status.ToString(),
                    CreatedAt = order.CreatedAt,
                    OrderPositions = orderPositionDtos
                }
            );
        }
        catch (Exception)
        {
            return Response.Fail(new UnknownError(), "Internal server error");
        }
    }
}
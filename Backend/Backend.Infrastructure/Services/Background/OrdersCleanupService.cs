using Backend.Application.DataAccess.Contexts;
using Backend.Domain.Models;
using Backend.Domain.Settings;
using Microsoft.EntityFrameworkCore;

namespace Backend.Infrastructure.Services.Background;

public class OrdersCleanupService
{
    private readonly MainDbContext _dbContext;

    public int BatchSize { get; }

    public OrdersCleanupService(OrderCleanupSettings settings, MainDbContext dbContext)
    {
        _dbContext = dbContext;
        BatchSize = settings.BatchSize;
    }


    public async Task CleanupExpiredOrdersAsync(CancellationToken cancellationToken = default)
    {
        DateTime currentTime = DateTime.UtcNow;

        List<int> expiredOrderIds = await _dbContext.Orders
            .Where(o => o.Status == OrderStatus.Pending &&
                        o.DeletionTime < currentTime)
            .OrderBy(o => o.DeletionTime)
            .Select(o => o.Id)
            .Take(BatchSize)
            .ToListAsync(cancellationToken);

        foreach (int orderId in expiredOrderIds)
        {
            await CleanupSingleOrderAsync(orderId, cancellationToken);
        }
    }

    private async Task CleanupSingleOrderAsync(int orderId, CancellationToken cancellationToken)
    {
        await using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            Domain.Models.Order? order = await _dbContext.Orders
                .Where(o => o.Status == OrderStatus.Pending)
                .Include(o => o.OrderedProducts)
                .ThenInclude(op => op.Product)
                .FirstOrDefaultAsync(o => o.Id == orderId, cancellationToken);

            if (order is null)
            {
                await transaction.RollbackAsync(cancellationToken);
                return;
            }

            foreach (OrderedProduct orderedProduct in order.OrderedProducts)
            {
                orderedProduct.Product.Quantity += orderedProduct.Quantity;
            }

            _dbContext.Orders.Remove(order);
            await _dbContext.SaveChangesAsync(cancellationToken);

            await transaction.CommitAsync(cancellationToken);
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }
}
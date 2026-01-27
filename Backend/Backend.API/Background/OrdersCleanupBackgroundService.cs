using Backend.Application.Services;

namespace Backend.API.Background;

public class OrdersCleanupBackgroundService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly TimeSpan _interval = TimeSpan.FromMinutes(1);
    
    public OrdersCleanupBackgroundService(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var scope = _scopeFactory.CreateScope();
                var cleanupService = scope.ServiceProvider.GetRequiredService<OrdersCleanupService>();

                await cleanupService.CleanupExpiredOrdersAsync(stoppingToken);
            }
            catch (Exception ex)
            {
                // логирование ошибок, чтобы не убить цикл
                Console.WriteLine($"Error in ReservationCleanupHostedService: {ex}");
            }
            
            try
            {
                await Task.Delay(_interval, stoppingToken);
            }
            catch (TaskCanceledException)
            {
                break;
            }
        }
    }
}
using Backend.Application.Common;
using Backend.Infrastructure;
using Backend.Domain.Settings;
using Backend.Infrastructure.Services.Background;


namespace Backend.API.Background;

public class OrdersCleanupBackgroundService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly OrderCleanupSettings _settings;

    public OrdersCleanupBackgroundService(IServiceScopeFactory scopeFactory, OrderCleanupSettings settings)
    {
        _scopeFactory = scopeFactory;
        _settings = settings;
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
                await Task.Delay(_settings.Interval, stoppingToken);
            }
            catch (TaskCanceledException)
            {
                break;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in ReservationCleanupHostedService: {ex}");
            }
        }
    }
}
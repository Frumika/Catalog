using Backend.Domain.Settings;
using Backend.Infrastructure.Services.Background;

namespace Backend.API.Background;

public class TokenCleanupBackgroundService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly TokenCleanupSettings _settings;

    public TokenCleanupBackgroundService(IServiceScopeFactory scopeFactory, TokenCleanupSettings settings)
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
                var cleanupService = scope.ServiceProvider.GetRequiredService<TokenCleanupService>();

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
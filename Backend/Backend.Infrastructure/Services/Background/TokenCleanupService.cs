using Backend.Application.DataAccess.Contexts;
using Backend.Domain.Models;
using Backend.Domain.Settings;
using Microsoft.EntityFrameworkCore;

namespace Backend.Infrastructure.Services.Background;

public class TokenCleanupService
{
    private readonly MainDbContext _dbContext;

    public int BatchSize { get; }

    public TokenCleanupService(TokenCleanupSettings settings, MainDbContext dbContext)
    {
        _dbContext = dbContext;
        BatchSize = settings.BatchSize;
    }


    public async Task CleanupExpiredOrdersAsync(CancellationToken cancellationToken = default)
    {
        DateTime currentTime = DateTime.UtcNow;
        await _dbContext.RefreshTokens
            .Where(rt => rt.ExpiresAt < currentTime || rt.IsRevoked)
            .OrderBy(rt => rt.ExpiresAt)
            .Take(BatchSize)
            .ExecuteDeleteAsync(cancellationToken);
    }
}
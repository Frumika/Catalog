namespace Backend.Domain.Settings;

public class TokenCleanupSettings
{
    public TimeSpan Interval { get; set; }
    public int BatchSize { get; set; }
}
namespace Backend.Domain.Settings;

public class OrderCleanupSettings
{
    public TimeSpan Interval { get; set; }
    public int BatchSize { get; set; }
}
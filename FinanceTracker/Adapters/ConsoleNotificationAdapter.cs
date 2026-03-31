using Microsoft.Extensions.Logging;

namespace FinanceTracker.Api.Adapters;

public class ConsoleNotificationAdapter : INotificationService
{
    private readonly ILogger<ConsoleNotificationAdapter> _logger;

    public ConsoleNotificationAdapter(ILogger<ConsoleNotificationAdapter> logger)
    {
        _logger = logger;
    }

    public Task SendNotificationAsync(string recipient, string message)
    {
        _logger.LogWarning("ALERT for {Recipient}: {Message}", recipient, message);
        Console.WriteLine($"\n[NOTIFICATION] To {recipient}: {message}\n");
        return Task.CompletedTask;
    }
}

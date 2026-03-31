namespace FinanceTracker.Api.Adapters;

public interface INotificationService
{
    Task SendNotificationAsync(string recipient, string message);
}


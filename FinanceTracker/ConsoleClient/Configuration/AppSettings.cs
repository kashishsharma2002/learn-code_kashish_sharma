namespace FinanceTracker.ConsoleClient.Configuration;

public class AppSettings
{
    public ApiSettings Api { get; set; } = new();
}


public class ApiSettings
{
    public string BaseUrl { get; set; } = string.Empty;
    public int TimeoutSeconds { get; set; } = 10;
}

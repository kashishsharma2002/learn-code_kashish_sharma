namespace FinanceTracker.ConsoleClient.Models;

public class AddTransactionRequest
{
    public Guid UserId { get; set; }
    public int Type { get; set; }
    public decimal Amount { get; set; }
    public int Category { get; set; }
    public DateTime Date { get; set; }
    public string Description { get; set; } = string.Empty;
}

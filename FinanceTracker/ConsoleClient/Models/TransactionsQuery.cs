namespace FinanceTracker.ConsoleClient.Models;

public class TransactionsQuery
{
    public Guid UserId { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int? Category { get; set; }
}

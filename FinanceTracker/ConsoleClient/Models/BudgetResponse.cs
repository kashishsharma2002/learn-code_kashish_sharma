namespace FinanceTracker.ConsoleClient.Models;

public class BudgetResponse
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public int Category { get; set; }
    public decimal MonthlyLimit { get; set; }
    public int Month { get; set; }
    public int Year { get; set; }
}

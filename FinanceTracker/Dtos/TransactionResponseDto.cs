namespace FinanceTracker.Api.Dtos;

public class TransactionResponseDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public decimal Amount { get; set; }
    public int Type { get; set; }
    public int Category { get; set; }
    public DateTime Date { get; set; }
    public string Description { get; set; } = string.Empty;
    public string? AlertMessage { get; set; }
}

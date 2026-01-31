namespace OrderProcessingApp.Models;

public class PaymentResult
{
    public bool IsSuccessful { get; init; }
    public string? TransactionId { get; init; }
    public string? ErrorMessage { get; init; }
}

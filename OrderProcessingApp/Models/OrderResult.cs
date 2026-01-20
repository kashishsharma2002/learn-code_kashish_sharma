namespace OrderProcessingApp.Models;

public class OrderResult
{
    public bool IsSuccessful { get; }
    public string Message { get; }
    public string? TransactionId { get; }

    private OrderResult(bool success, string message, string? transactionId = null)
    {
        IsSuccessful = success;
        Message = message;
        TransactionId = transactionId;
    }

    public static OrderResult Success(string transactionId) =>
        new(true, "Order processed successfully", transactionId);

    public static OrderResult Failed(string message) =>
        new(false, message);

    public static OrderResult Invalid(string message) =>
        new(false, message);
}

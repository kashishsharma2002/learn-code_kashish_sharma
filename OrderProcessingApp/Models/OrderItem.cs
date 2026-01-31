namespace OrderProcessingApp.Models;

public class OrderItem
{
    public required string ProductId { get; init; }
    public int Quantity { get; init; }
    public decimal Price { get; init; }
}

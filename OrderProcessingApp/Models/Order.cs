using System.Collections.Generic;

namespace OrderProcessingApp.Models;

public class Order
{
    public required string OrderId { get; init; }
    public required string CustomerId { get; init; }
    public required List<OrderItem> Items { get; init; }
    public decimal TotalAmount { get; init; }
    public PaymentMethod PaymentMethod { get; set; }
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    public string? TransactionId { get; set; }
}

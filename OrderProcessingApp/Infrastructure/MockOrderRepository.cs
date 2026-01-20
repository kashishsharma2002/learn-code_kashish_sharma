using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OrderProcessingApp.Interfaces;
using OrderProcessingApp.Models;

namespace OrderProcessingApp.Infrastructure;

public class MockOrderRepository : IOrderRepository
{
    private readonly Dictionary<string, Order> _orders = new();

    public Task<Order?> GetById(string orderId)
    {
        _orders.TryGetValue(orderId, out var order);
        return Task.FromResult(order);
    }

    public Task Save(Order order)
    {
        _orders[order.OrderId] = order;
        Console.WriteLine($"Order {order.OrderId} saved with status {order.Status}");
        return Task.CompletedTask;
    }
}


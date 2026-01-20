using System;
using System.Threading.Tasks;
using OrderProcessingApp.Interfaces;
using OrderProcessingApp.Models;

namespace OrderProcessingApp.Infrastructure
{
    public class MockNotificationService : INotificationService
    {
        public Task SendOrderConfirmation(Order order)
        {
            Console.WriteLine($"Sending confirmation email for order {order.OrderId}");
            return Task.CompletedTask;
        }
    }
}

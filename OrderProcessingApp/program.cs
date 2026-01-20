using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OrderProcessingApp.Infrastructure;
using OrderProcessingApp.Models;
using OrderProcessingApp.Services;

namespace OrderProcessingApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var processor = BuildOrderProcessor();
            await RunDemo(processor);
        }

        private static OrderProcessor BuildOrderProcessor()
        {
            return new OrderProcessor(
                new MockPaymentGateway(),
                new MockInventoryService(),
                new MockNotificationService(),
                new MockOrderRepository(),
                new OrderValidator());
        }

        private static async Task RunDemo(OrderProcessor processor)
        {
            var order = CreateSampleOrder();

            var result = await processor.ProcessOrderAsync(order);
            Console.WriteLine(result.Message);

            Console.WriteLine("\n" + new string('-', 50) + "\n");

            await processor.CancelOrderAsync(order.OrderId);
        }

        private static Order CreateSampleOrder()
        {
            return new Order
            {
                OrderId = "ORD-001",
                CustomerId = "CUST-100",
                Items = new List<OrderItem>
                {
                    new() { ProductId = "P1", Quantity = 2, Price = 50 },
                    new() { ProductId = "P2", Quantity = 1, Price = 30 }
                },
                TotalAmount = 130,
                PaymentMethod = "CARD"
            };
        }
    }
}

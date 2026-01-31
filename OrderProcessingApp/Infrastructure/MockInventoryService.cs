using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OrderProcessingApp.Interfaces;
using OrderProcessingApp.Models;

namespace OrderProcessingApp.Infrastructure
{
    public class MockInventoryService : IInventoryService
    {
        public Task<bool> CheckAvailability(List<OrderItem> items)
        {
            Console.WriteLine($"Checking availability for {items.Count} items");
            return Task.FromResult(true);
        }
        // Mock inventory service: method parameters are unused as this is a demo implementation 
        public Task ReserveItems(List<OrderItem> _)
        {
            Console.WriteLine("Items reserved");
            return Task.CompletedTask;
        }

        public Task CommitReservation(List<OrderItem> _)
        {
            Console.WriteLine("Reservation committed");
            return Task.CompletedTask;
        }

        public Task ReleaseReservation(List<OrderItem> _)
        {
            Console.WriteLine("Reservation released");
            return Task.CompletedTask;
        }

        public Task RestoreInventory(List<OrderItem> _)
        {
            Console.WriteLine("Inventory restored");
            return Task.CompletedTask;
        }
    }
}

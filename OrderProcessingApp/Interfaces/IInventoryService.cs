using System.Collections.Generic;
using System.Threading.Tasks;
using OrderProcessingApp.Models;

namespace OrderProcessingApp.Interfaces
{
    public interface IInventoryService
    {
        Task<bool> CheckAvailability(List<OrderItem> items);
        Task ReserveItems(List<OrderItem> items);
        Task CommitReservation(List<OrderItem> items);
        Task ReleaseReservation(List<OrderItem> items);
        Task RestoreInventory(List<OrderItem> items);
    }
}

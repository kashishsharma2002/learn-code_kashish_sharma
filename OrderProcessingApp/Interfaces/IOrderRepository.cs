using System.Threading.Tasks;
using OrderProcessingApp.Models;

namespace OrderProcessingApp.Interfaces
{
   public interface IOrderRepository
    {
        Task<Order?> GetById(string orderId);
        Task Save(Order order);
    }

}

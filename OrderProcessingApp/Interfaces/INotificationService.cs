using System.Threading.Tasks;
using OrderProcessingApp.Models;

namespace OrderProcessingApp.Interfaces
{
    public interface INotificationService
    {
        Task SendOrderConfirmation(Order order);
    }
}

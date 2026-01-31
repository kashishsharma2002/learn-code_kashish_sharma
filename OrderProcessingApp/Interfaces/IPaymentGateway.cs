using System.Threading.Tasks;
using OrderProcessingApp.Models;

namespace OrderProcessingApp.Interfaces
{
    public interface IPaymentGateway
    {
        Task<PaymentResult> ProcessPayment(string customerId, decimal amount, string paymentMethod);
        Task<bool> IsRefundSuccessful(string transactionId);
    }
}

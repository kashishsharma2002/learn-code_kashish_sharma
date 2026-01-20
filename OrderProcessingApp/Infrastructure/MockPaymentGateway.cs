using System;
using System.Threading.Tasks;
using OrderProcessingApp.Interfaces;
using OrderProcessingApp.Models;

namespace OrderProcessingApp.Infrastructure
{
    public class MockPaymentGateway : IPaymentGateway
    {
        public Task<PaymentResult> ProcessPayment(string customerId, decimal amount, string paymentMethod)
        {
            Console.WriteLine($"Processing payment: ${amount} for customer {customerId}");
            return Task.FromResult(new PaymentResult
            {
                IsSuccessful = true,
                TransactionId = "TXN123456"
            });
        }

        public Task<bool> RefundPayment(string transactionId)
        {
            Console.WriteLine($"Refunding transaction: {transactionId}");
            return Task.FromResult(true);
        }
    }
}

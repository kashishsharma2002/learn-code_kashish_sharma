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
            // Mock implementation: simulates basic payment validation for demo
            Console.WriteLine($"Processing payment: ${amount} for customer {customerId}");
            bool isSuccessful = !string.IsNullOrEmpty(customerId) 
                && amount > 0 
                && !string.IsNullOrEmpty(paymentMethod);
            return Task.FromResult(new PaymentResult
            {
                IsSuccessful = isSuccessful,
                TransactionId = isSuccessful ? "TXN123456" : null
            });
        }

        public Task<bool> IsRefundSuccessful(string transactionId)
        {
            Console.WriteLine($"Refunding transaction: {transactionId}");
            return Task.FromResult(true);
        }
    }
}

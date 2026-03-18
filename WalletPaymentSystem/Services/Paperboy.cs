using WalletPaymentSystem.Models;

namespace WalletPaymentSystem.Services;

public class Paperboy
{
    public PaymentReceipt CollectPayment(Customer customer, decimal paymentAmount)
    {
        return customer.AttemptPayment(paymentAmount);
    }
}


using WalletPaymentSystem.Models;
using WalletPaymentSystem.Services;

namespace WalletPaymentSystem;

class Program
{
    static void Main(string[] args)
    {
        var customer = new Customer("Kashish", "Sharma", new Wallet(25.00m));
        var paperboy = new Paperboy();

        var receipt = paperboy.CollectPayment(customer, 10.00m);

        Console.WriteLine($"Payment successful: {receipt.WasPaid}");
        Console.WriteLine($"Balance before: {receipt.BalanceBefore:C}");
        Console.WriteLine($"Balance after: {receipt.BalanceAfter:C}");
    }
}
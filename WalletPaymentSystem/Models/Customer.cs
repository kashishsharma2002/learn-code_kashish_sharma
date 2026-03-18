namespace WalletPaymentSystem.Models;

public class Customer
{
    private string _firstName;
    private string _lastName;
    private Wallet _myWallet;

    public Customer(string firstName, string lastName, Wallet wallet)
    {
        _firstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
        _lastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
        _myWallet = wallet ?? throw new ArgumentNullException(nameof(wallet));
    }

    public string GetFullName()
    {
        return $"{_firstName} {_lastName}";
    }

    public PaymentReceipt AttemptPayment(decimal amount)
    {
        decimal balanceBefore = _myWallet.GetBalance();

        try
        {
            _myWallet.Deduct(amount);
            decimal balanceAfter = _myWallet.GetBalance();
            return new PaymentReceipt(wasPaid: true, amount, balanceBefore, balanceAfter);
        }
        catch (InvalidOperationException)
        {
            return new PaymentReceipt(wasPaid: false, amount, balanceBefore, balanceBefore);
        }
    }
}

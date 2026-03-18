namespace WalletPaymentSystem.Models;

public sealed class Wallet
{
    private const decimal MinimumAmount = 0m;
    private decimal _balance;

    public Wallet(decimal initialAmount)
    {
        if (initialAmount < MinimumAmount)
        {
            throw new ArgumentException(
                $"Initial amount cannot be negative. Provided: {initialAmount}",
                nameof(initialAmount));
        }

        _balance = initialAmount;
    }

    public decimal GetBalance()
    {
        return _balance;
    }

    public bool CanAfford(decimal amount)
    {
        ValidateAmount(amount);
        return _balance >= amount;
    }

    public void Deduct(decimal amount)
    {
        ValidateAmount(amount);

        if (!CanAfford(amount))
        {
            throw new InvalidOperationException(
                $"Insufficient funds. Balance: {_balance:C}, Requested: {amount:C}");
        }

        _balance -= amount;
    }

    private void ValidateAmount(decimal amount)
    {
        if (amount <= MinimumAmount)
            throw new ArgumentException($"Amount must be positive. Provided: {amount}", nameof(amount));
    }
}

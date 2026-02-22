namespace BankingManagementSystem.Models;

public class Account
{
    public int AccountId { get; set; }
    public AccountType AccountType { get; }
    public decimal Balance { get; private set; }
    public AccountStatus Status { get; private set; }
    public int CustomerId { get; }
    public DateTime CreatedDate { get; }

    public Account(int customerId, AccountType accountType)
    {
        CustomerId = customerId;
        AccountType = accountType;
        Status = AccountStatus.Active;
        CreatedDate = DateTime.UtcNow;
        Balance = 0;
    }

    public void Deposit(decimal amount)
    {
        if (Status == AccountStatus.Closed)
            throw new InvalidOperationException("Cannot deposit to a closed account.");

        if (amount <= 0)
            throw new ArgumentException("Amount must be positive.");

        Balance += amount;
    }

    public void Withdraw(decimal amount)
    {
        if (Status == AccountStatus.Closed)
            throw new InvalidOperationException("Cannot withdraw from a closed account.");

        if (amount <= 0)
            throw new ArgumentException("Amount must be positive.");

        if (amount > Balance)
            throw new InvalidOperationException("Insufficient funds.");

        Balance -= amount;
    }

    public void Close()
    {
        if (Status == AccountStatus.Closed)
            throw new InvalidOperationException("Account is already closed.");

        if (Balance > 0)
            throw new InvalidOperationException("Withdraw remaining balance before closing account.");

        Status = AccountStatus.Closed;
    }
}

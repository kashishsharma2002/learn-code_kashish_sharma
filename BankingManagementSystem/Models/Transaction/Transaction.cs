namespace BankingManagementSystem.Models;

public class Transaction
{
    public int TransactionId { get; set; }
    public int AccountId { get; set; }
    public decimal Amount { get; set; }
    public DateTime TransactionDate { get; set; }
    public TransactionType TransactionType { get; set; }
    public PaymentMethod PaymentMethod { get; set; }

    public void Apply(Account account)
    {
        if (account == null)
            throw new ArgumentNullException(nameof(account));

        if (Amount <= 0)
            throw new ArgumentException("Transaction amount must be positive.");

        switch (TransactionType)
        {
            case TransactionType.Deposit:
                account.Deposit(Amount);
                break;

            case TransactionType.Withdrawal:
                account.Withdraw(Amount);
                break;

            default:
                throw new InvalidOperationException(
                    "Transfer must be handled separately.");
        }
    }
}

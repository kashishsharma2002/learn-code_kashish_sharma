namespace BankingManagementSystem.Models;

public class Transaction
{
    public int TransactionId { get; set; }
    public int AccountId { get; set; }
    public decimal Amount { get; set; }
    public DateTime TransactionDate { get; set; }
    public TransactionType TransactionType { get; set; }
    public PaymentMethod PaymentMethod { get; set; }

    public Transaction()
    {
    }

    public Transaction(Transaction t)
    {
        TransactionId = t.TransactionId;
        AccountId = t.AccountId;
        Amount = t.Amount;
        TransactionDate = t.TransactionDate;
        TransactionType = t.TransactionType;
        PaymentMethod = t.PaymentMethod;
    }
}
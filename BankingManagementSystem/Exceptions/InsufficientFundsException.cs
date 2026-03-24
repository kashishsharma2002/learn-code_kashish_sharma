namespace BankingManagementSystem.Exceptions;

public class InsufficientFundsException : BankingException
{
    public decimal RequiredAmount { get; }
    public decimal AvailableBalance { get; }

    public InsufficientFundsException(decimal required, decimal available)
        : base($"Insufficient balance. Required: {required}, Available: {available}")
    {
        RequiredAmount = required;
        AvailableBalance = available;
    }
}

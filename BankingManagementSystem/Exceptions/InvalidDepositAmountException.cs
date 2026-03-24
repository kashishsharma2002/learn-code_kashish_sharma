namespace BankingManagementSystem.Exceptions;

public class InvalidDepositAmountException : BankingException
{
    public decimal MinimumAmount { get; }
    public decimal ProvidedAmount { get; }

    public InvalidDepositAmountException(decimal minimum, decimal provided, string accountType)
        : base($"Initial deposit for {accountType} account must be at least {minimum}. Provided: {provided}")
    {
        MinimumAmount = minimum;
        ProvidedAmount = provided;
    }
}

namespace BankingManagementSystem.Exceptions;

public class BankingException : Exception
{
    public BankingException(string message) : base(message) { }
}

public class CustomerNotFoundException : BankingException
{
    public int CustomerId { get; }

    public CustomerNotFoundException(int customerId)
        : base($"Customer with ID {customerId} not found")
    {
        CustomerId = customerId;
    }
}
public class AccountNotFoundException : BankingException
{
    public int AccountId { get; }

    public AccountNotFoundException(int accountId)
        : base($"Account with ID {accountId} not found")
    {
        AccountId = accountId;
    }
}

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

public class LoanEligibilityException : BankingException
{
    public LoanEligibilityException(string reason)
        : base($"Loan eligibility check failed: {reason}")
    {
    }
}

public class InvalidBankingOperationException : BankingException
{
    public InvalidBankingOperationException(string message) : base(message) { }
}

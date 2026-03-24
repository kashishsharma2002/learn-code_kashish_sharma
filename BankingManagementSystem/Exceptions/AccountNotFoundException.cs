namespace BankingManagementSystem.Exceptions;

public class AccountNotFoundException : BankingException
{
    public int AccountId { get; }

    public AccountNotFoundException(int accountId)
        : base($"Account with ID {accountId} not found")
    {
        AccountId = accountId;
    }
}

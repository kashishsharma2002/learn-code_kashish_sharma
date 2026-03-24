namespace BankingManagementSystem.Exceptions;

public class InvalidBankingOperationException : BankingException
{
    public InvalidBankingOperationException(string message) : base(message) { }
}

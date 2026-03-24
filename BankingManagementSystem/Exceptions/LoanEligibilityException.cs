namespace BankingManagementSystem.Exceptions;

public class LoanEligibilityException : BankingException
{
    public LoanEligibilityException(string reason)
        : base($"Loan eligibility check failed: {reason}")
    {
    }
}

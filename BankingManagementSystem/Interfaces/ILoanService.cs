using BankingManagementSystem.Models;

namespace BankingManagementSystem.Interfaces;

public interface ILoanService
{
    Loan ApplyForLoan(Loan loan);
    Loan GetLoanDetails(int loanId);
}
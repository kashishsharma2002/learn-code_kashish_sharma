using BankingManagementSystem.Models;

namespace BankingManagementSystem.Interfaces;

public interface ILoanRepository
{
    void Add(Loan loan);
    Loan? GetLoanByCustomerId(int customerId);
    IEnumerable<Loan> GetAll();
}
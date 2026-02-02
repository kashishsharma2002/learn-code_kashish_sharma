using BankingManagementSystem.Models;
using BankingManagementSystem.Interfaces;

namespace BankingManagementSystem.Repositories;

public class LoanRepository : ILoanRepository
{
    private readonly List<Loan> _loans = new List<Loan>();
    private int _nextId = 1;

    public void Add(Loan loan)
    {
        loan.LoanId = _nextId++;
        _loans.Add(loan);
    }

    public Loan? GetLoanByCustomerId(int customerId)
    {
        return _loans.FirstOrDefault(t => t.CustomerId == customerId);
    }

    public IEnumerable<Loan> GetAll()
    {
        return _loans;
    }

    public void Remove(int loanId)
    {
        var loan = _loans.FirstOrDefault(t => t.LoanId == loanId);
        if (loan != null)
        {
            _loans.Remove(loan);
        }
    }
}

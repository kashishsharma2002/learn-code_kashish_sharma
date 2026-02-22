using BankingManagementSystem.Models;
using BankingManagementSystem.Interfaces;
using BankingManagementSystem.Constants;
using BankingManagementSystem.Exceptions;

namespace BankingManagementSystem.Services;

public class LoanService : ILoanService
{
    private readonly ILoanRepository _loanRepository;
    private readonly ICustomerRepository _customerRepository;
    private static int _loanIdCounter = 1;

    public LoanService(ILoanRepository loanRepository, ICustomerRepository customerRepository)
    {
        _loanRepository = loanRepository;
        _customerRepository = customerRepository;
    }

    public Loan ApplyForLoan(Loan loan)
    {
        ArgumentNullException.ThrowIfNull(loan);

        var customer = _customerRepository.GetById(loan.CustomerId);

        if (customer == null)
            throw new CustomerNotFoundException(loan.CustomerId);

        loan.LoanId = _loanIdCounter++;

        int age = CalculateCustomerAge(loan.CustomerId);

        loan.EvaluateEligibility(age);

        _loanRepository.Add(loan);

        return loan;
    }


    public Loan GetLoanDetails(int customerId)
    {
        var loan = _loanRepository.GetLoanByCustomerId(customerId);

        if (loan == null)
        {
            throw new InvalidBankingOperationException(
                $"No loan found for customer {customerId}"
            );
        }
        return loan;
    }

    private int CalculateCustomerAge(int customerId)
    {
        var customer = _customerRepository.GetById(customerId);

        if (customer == null)
            throw new CustomerNotFoundException(customerId);

        var today = DateTime.Today;
        var dob = customer.Profile.BasicDetails.DateOfBirth;
        var age = today.Year - dob.Year;

        if (dob.Date > today.AddYears(-age))
            age--;

        return age;
    }

}
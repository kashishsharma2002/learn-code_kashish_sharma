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
        ArgumentNullException.ThrowIfNull(loan, nameof(loan));

        var customer = _customerRepository.GetById(loan.CustomerId);
        if (customer == null)
            throw new CustomerNotFoundException(loan.CustomerId);

        loan.LoanId = _loanIdCounter++;
        loan.ApplicationDate = DateTime.Now;
        var interestRate = GetInterestRate(loan.Type);
        loan.InterestRate = interestRate;

        if (CheckLoanEligibility(loan))
        {
            loan.IsEligible = true;
            loan.Status = LoanStatus.Approved;
            
            CalculateInterest(loan);
        }
        else
        {
            loan.IsEligible = false;
            loan.Status = LoanStatus.Rejected;
        }

        _loanRepository.Add(loan);
        return loan;
    }

    public bool CheckLoanEligibility(Loan loan)
    {
        var maxAmount = GetMaxLoanAmount(loan.Type);
        var minAge = GetMinEligibleAge(loan.Type);
        var customerAge = GetCustomerAge(loan.CustomerId);

        if (loan.Amount > maxAmount)
            return false;

        if (customerAge < minAge)
            return false;

        return true;
    }

    private decimal GetMaxLoanAmount(LoanType type)
    {
        return type switch
        {
            LoanType.Personal => LoanEligibilityCriteria.MaxPersonalLoanAmount,
            LoanType.Home => LoanEligibilityCriteria.MaxHomeLoanAmount,
            LoanType.Education => LoanEligibilityCriteria.MaxEducationLoanAmount,
            LoanType.Business => LoanEligibilityCriteria.MaxBusinessLoanAmount,
            LoanType.Medical => LoanEligibilityCriteria.MaxMedicalLoanAmount,
            _ => throw new ArgumentException("Invalid loan type")
        };
    }

    private int GetMinEligibleAge(LoanType type)
    {
        return type switch
        {
            LoanType.Personal => LoanEligibilityCriteria.MinEligibleAgePersonalLoan,
            LoanType.Home => LoanEligibilityCriteria.MinEligibleAgeHomeLoan,
            LoanType.Education => LoanEligibilityCriteria.MinEligibleAgeEducationLoan,
            LoanType.Business => LoanEligibilityCriteria.MinEligibleAgeBusinessLoan,
            LoanType.Medical => LoanEligibilityCriteria.MinEligibleAgeMedicalLoan,
            _ => throw new ArgumentException("Invalid loan type")
        };
    }

    private int GetCustomerAge(int customerId)
    {
        var customer = _customerRepository.GetById(customerId);
        if (customer == null)
            throw new CustomerNotFoundException(customerId);

        var today = DateTime.Today;
        var age = today.Year - customer.Profile.BasicDetails.DateOfBirth.Year;

        if (customer.Profile.BasicDetails.DateOfBirth.Date > today.AddYears(-age))
            age--;

        return age;
    }

    public Loan GetLoanDetails(int customerId)
    {
        var loan = _loanRepository.GetLoanByCustomerId(customerId);
        if (loan == null)
            throw new InvalidBankingOperationException($"No loan found for customer {customerId}");
        return loan;
    }

    public IEnumerable<Loan> GetAllLoans()
    {
        var loansList = _loanRepository.GetAll();
        if (!loansList.Any())
            throw new InvalidBankingOperationException("No loans found");
        return loansList;
    }

    private double GetInterestRate(LoanType type)
    {
        return type switch
        {
            LoanType.Personal => LoanInterestRates.PersonalLoanRate,
            LoanType.Home => LoanInterestRates.HomeLoanRate,
            LoanType.Education => LoanInterestRates.EducationLoanRate,
            LoanType.Business => LoanInterestRates.BusinessLoanRate,
            LoanType.Medical => LoanInterestRates.MedicalLoanRate,
            _ => throw new ArgumentException("Invalid loan type")
        };
    }
    private void CalculateInterest(Loan loan)
    {
        if (loan.Status != LoanStatus.Approved)
            return;

        double timeInYears = loan.TermInMonths / 12.0;

        decimal interest = loan.Amount * (decimal)loan.InterestRate * (decimal)timeInYears / 100;
        
        loan.CalculatedInterest = Math.Round(interest, 2);
        loan.TotalRepayAmount = loan.Amount + loan.CalculatedInterest;
        
        loan.MonthlyInstallment = Math.Round(loan.TotalRepayAmount / loan.TermInMonths, 2);
    }
}
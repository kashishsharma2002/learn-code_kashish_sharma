using BankingManagementSystem.Constants;
using BankingManagementSystem.Exceptions;

namespace BankingManagementSystem.Models;

public class Loan
{
    public int LoanId { get; set; }
    public int CustomerId { get; }
    public int AccountId { get; }
    public decimal Amount { get; }
    public double InterestRate { get; private set; }
    public int TermInMonths { get; }
    public DateTime ApplicationDate { get; }
    public DateTime? StartDate { get; private set; }
    public DateTime? EndDate { get; private set; }
    public bool IsEligible { get; private set; }
    public LoanType Type { get; }
    public LoanStatus Status { get; private set; }

    public decimal CalculatedInterest { get; private set; }
    public decimal TotalRepayAmount { get; private set; }
    public decimal MonthlyInstallment { get; private set; }

    public Loan(int customerId, int accountId, decimal amount, int termInMonths, LoanType type)
    {
        if (customerId <= 0)
            throw new ArgumentException("Invalid customer id");

        if (accountId <= 0)
            throw new ArgumentException("Invalid account id");

        if (amount <= 0)
            throw new ArgumentException("Loan amount must be positive");

        if (termInMonths <= 0)
            throw new ArgumentException("Term must be greater than zero");

        CustomerId = customerId;
        AccountId = accountId;
        Amount = amount;
        TermInMonths = termInMonths;
        Type = type;

        Status = LoanStatus.Pending;
        ApplicationDate = DateTime.UtcNow;
    }

    public void EvaluateEligibility(int customerAge)
    {
        if (Status != LoanStatus.Pending)
            throw new InvalidOperationException("Loan already processed.");

        var maxAmount = GetMaxLoanAmount();
        var minAge = GetMinEligibleAge();

        if (Amount > maxAmount)
        {
            throw new LoanEligibilityException(
                $"Loan amount ${Amount} exceeds maximum allowed amount ${maxAmount} for {Type} loan");
        }

        if (customerAge < minAge)
        {
            throw new LoanEligibilityException(
                $"Customer age {customerAge} is below minimum required age {minAge} for {Type} loan");
        }

        Approve();
    }

    private void Approve()
    {
        IsEligible = true;
        Status = LoanStatus.Approved;

        AssignInterestRate();
        CalculateRepayment();

        StartDate = DateTime.UtcNow;
        EndDate = StartDate.Value.AddMonths(TermInMonths);
    }

    private void Reject()
    {
        IsEligible = false;
        Status = LoanStatus.Rejected;
    }

    private void AssignInterestRate()
    {
        InterestRate = Type switch
        {
            LoanType.Personal => LoanInterestRates.PersonalLoanRate,
            LoanType.Home => LoanInterestRates.HomeLoanRate,
            LoanType.Education => LoanInterestRates.EducationLoanRate,
            LoanType.Business => LoanInterestRates.BusinessLoanRate,
            LoanType.Medical => LoanInterestRates.MedicalLoanRate,
            _ => throw new ArgumentException("Invalid loan type")
        };
    }

    private void CalculateRepayment()
    {
        var years = TermInMonths / 12.0;

        CalculatedInterest = Math.Round(
            Amount * (decimal)InterestRate * (decimal)years / 100,
            2
        );

        TotalRepayAmount = Amount + CalculatedInterest;

        MonthlyInstallment = Math.Round(
            TotalRepayAmount / TermInMonths,
            2
        );
    }

    private decimal GetMaxLoanAmount()
    {
        return Type switch
        {
            LoanType.Personal => LoanEligibilityCriteria.MaxPersonalLoanAmount,
            LoanType.Home => LoanEligibilityCriteria.MaxHomeLoanAmount,
            LoanType.Education => LoanEligibilityCriteria.MaxEducationLoanAmount,
            LoanType.Business => LoanEligibilityCriteria.MaxBusinessLoanAmount,
            LoanType.Medical => LoanEligibilityCriteria.MaxMedicalLoanAmount,
            _ => throw new ArgumentException("Invalid loan type")
        };
    }

    private int GetMinEligibleAge()
    {
        return Type switch
        {
            LoanType.Personal => LoanEligibilityCriteria.MinEligibleAgePersonalLoan,
            LoanType.Home => LoanEligibilityCriteria.MinEligibleAgeHomeLoan,
            LoanType.Education => LoanEligibilityCriteria.MinEligibleAgeEducationLoan,
            LoanType.Business => LoanEligibilityCriteria.MinEligibleAgeBusinessLoan,
            LoanType.Medical => LoanEligibilityCriteria.MinEligibleAgeMedicalLoan,
            _ => throw new ArgumentException("Invalid loan type")
        };
    }
}
using BankingManagementSystem.Models;
using BankingManagementSystem.Interfaces;
using BankingManagementSystem.Common;
using BankingManagementSystem.Exceptions;

namespace BankingManagementSystem.Controllers;

public class LoanController
{
    private readonly ILoanService _loanService;
    private readonly IInputReader _inputReader;

    public LoanController(ILoanService loanService, IInputReader inputReader)
    {
        _loanService = loanService;
        _inputReader = inputReader;
    }

    public void ApplyLoanApplication()
    {
        try
        {
            var loan = ReadLoanDetails();
            var appliedLoan = _loanService.ApplyForLoan(loan);

            DisplayLoanApplication(appliedLoan);
            DisplayLoanResult(appliedLoan);
        }
        catch (LoanEligibilityException ex)
        {
            Console.WriteLine($"Loan Application Rejected: {ex.Message}");
        }
        catch (CustomerNotFoundException ex)
        {
            Console.WriteLine($"Error: Customer with ID {ex.CustomerId} not found");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error applying for loan: {ex.Message}");
        }
    }

    private Loan ReadLoanDetails()
    {
        var customerId = _inputReader.ReadInt("Enter Customer ID:");
        var accountId = _inputReader.ReadInt("Enter Account ID:");

        if (!customerId.HasValue || !accountId.HasValue)
            throw new InvalidOperationException("Customer ID and Account ID are required");

        var amount = _inputReader.ReadDecimal("Enter Loan Amount:");
        var termInMonths = _inputReader.ReadInt("Enter Term (in months):");

        if (!termInMonths.HasValue)
            throw new InvalidOperationException("Term is required");

        var loanType = ReadLoanType("Enter Loan Type:");

        return new Loan(
            customerId.Value,
            accountId.Value,
            amount,
            termInMonths.Value,
            loanType
        );
    }

    private LoanType ReadLoanType(string prompt)
    {
        while (true)
        {
            var input = _inputReader.ReadRequiredString(prompt);

            if (Enum.TryParse<LoanType>(input, true, out var loanType))
                return loanType;

            Console.WriteLine("Invalid loan type. Try again.");
        }
    }

    private void DisplayLoanApplication(Loan appliedLoan)
    {
        Console.WriteLine("\n=== LOAN APPLICATION RESULT ===");
        Console.WriteLine($"Loan ID: {appliedLoan.LoanId}");
        Console.WriteLine($"Loan Amount: {appliedLoan.Amount:C}");
        Console.WriteLine($"Status: {appliedLoan.Status}");
    }

    private void DisplayLoanResult(Loan loan)
    {
        if (loan.Status == LoanStatus.Approved)
        {
            DisplayApprovedLoan(loan);
            return;
        }

        Console.WriteLine("Loan rejected. Check eligibility.");
    }

    private void DisplayApprovedLoan(Loan loan)
    {
        Console.WriteLine("\n=== APPROVED LOAN DETAILS ===");
        Console.WriteLine($"Principal: {loan.Amount:C}");
        Console.WriteLine($"Rate: {loan.InterestRate}%");
        Console.WriteLine($"Term: {loan.TermInMonths} months");
        Console.WriteLine($"Interest: {loan.CalculatedInterest:C}");
        Console.WriteLine($"Total: {loan.TotalRepayAmount:C}");
        Console.WriteLine($"Monthly EMI: {loan.MonthlyInstallment:C}");
    }

    public void ViewLoanById()
    {
        try
        {
            var customerId = _inputReader.ReadInt("Enter Customer ID:");

            if (!customerId.HasValue)
                throw new InvalidOperationException("Customer ID is required");

            var loan = _loanService.GetLoanDetails(customerId.Value);

            DisplayLoanDetails(loan);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error retrieving loan: {ex.Message}");
        }
    }

    private void DisplayLoanDetails(Loan loan)
    {
        Console.WriteLine("-----------------------");
        Console.WriteLine($"Loan ID: {loan.LoanId}");
        Console.WriteLine($"Customer ID: {loan.CustomerId}");
        Console.WriteLine($"Account ID: {loan.AccountId}");
        Console.WriteLine($"Amount: {loan.Amount}");
        Console.WriteLine($"Interest Rate: {loan.InterestRate}%");
        Console.WriteLine($"Term: {loan.TermInMonths}");
        Console.WriteLine($"Start Date: {loan.StartDate}");
        Console.WriteLine($"End Date: {loan.EndDate}");
        Console.WriteLine($"Type: {loan.Type}");
        Console.WriteLine($"Status: {loan.Status}");
        Console.WriteLine("-----------------------");
    }
}

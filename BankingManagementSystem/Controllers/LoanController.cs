using BankingManagementSystem.Models;
using BankingManagementSystem.Interfaces;
using BankingManagementSystem.Common;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.Marshalling;
using System.Runtime.Loader;

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
            if (loan == null)
                throw new InvalidOperationException("Invalid loan details");

            var appliedLoan = _loanService.ApplyForLoan(loan);

            DisplayLoanApplication(appliedLoan);

            if (appliedLoan.Status == LoanStatus.Approved)
            {
                DisplayApprovedLoan(appliedLoan);
            }
            else
            {
                Console.WriteLine("Your loan application has been rejected. Please check eligibility criteria.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error applying for loan: {ex.Message}");
        }
    }
    private void DisplayLoanApplication(Loan appliedLoan)
    {
        Console.WriteLine("\n=== LOAN APPLICATION RESULT ===");
        Console.WriteLine($"Loan ID: {appliedLoan.LoanId}");
        Console.WriteLine($"Loan Amount: {appliedLoan.Amount:C}");
        Console.WriteLine($"Status: {appliedLoan.Status}");
    }

    private void DisplayApprovedLoan(Loan appliedLoan)
    {
        Console.WriteLine("\n=== APPROVED LOAN DETAILS ===");
        Console.WriteLine($"Principal: {appliedLoan.Amount:C}");
        Console.WriteLine($"Annual Interest Rate: {appliedLoan.InterestRate}%");
        Console.WriteLine($"Loan Term: {appliedLoan.TermInMonths} months");
        Console.WriteLine($"Calculated Interest (SI): {appliedLoan.CalculatedInterest:C}");
        Console.WriteLine($"Total Repayment Amount: {appliedLoan.TotalRepayAmount:C}");
        Console.WriteLine($"Monthly Installment: {appliedLoan.MonthlyInstallment:C}");
    }
    private Loan ReadLoanDetails()
    {
        var customerId = _inputReader.ReadInt("Enter Customer ID:");
        var accountId = _inputReader.ReadInt("Enter Account ID:");
        if (!customerId.HasValue || !accountId.HasValue)
            throw new InvalidOperationException("Customer ID and Account ID are required");
        var amount = _inputReader.ReadDecimal("Enter Loan Amount:");
        var termInMonths = _inputReader.ReadInt("Enter Term (in months):");
        var loanType = ReadLoanType("Enter Loan Type (Personal/Home/Medical/Education/Business):");
        if (!termInMonths.HasValue)
            throw new InvalidOperationException("Term is required");

        return new Loan
        {
            CustomerId = customerId.Value,
            AccountId = accountId.Value,
            Amount = amount,
            TermInMonths = termInMonths.Value,
            Type = loanType,
            ApplicationDate = DateTime.Now,
            Status = LoanStatus.Pending
        };
    }
    private LoanType ReadLoanType(string prompt)
    {
        while (true)
        {
            var input = _inputReader.ReadRequiredString(prompt);
            if (Enum.TryParse<LoanType>(input, true, out var loanType))
            {
                return loanType;
            }
            Console.WriteLine("Invalid loan type. Please try again.");
        }
    }

    public void ViewLoanById()
    {
        try
        {
            var loanId = _inputReader.ReadInt("Enter Loan ID:");
            if (!loanId.HasValue)
                throw new InvalidOperationException("Loan ID is required");
            var loan = _loanService.GetLoanDetails(loanId.Value);
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
        Console.WriteLine($"Term: {loan.TermInMonths} months");
        Console.WriteLine($"Start Date: {loan.StartDate}");
        Console.WriteLine($"End Date: {loan.EndDate}");
        Console.WriteLine($"Type: {loan.Type}");
        Console.WriteLine($"Status: {loan.Status}");
        Console.WriteLine("-----------------------");
    }
}
namespace BankingManagementSystem.Models;

public class Loan
{
    public int LoanId { get; set; }
    public int CustomerId { get; set; }
    public int AccountId { get; set; }
    public decimal Amount { get; set; }
    public double InterestRate { get; set; }
    public int TermInMonths { get; set; }
    public DateTime ApplicationDate { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsEligible { get; set; }
    public LoanType Type { get; set; }
    public LoanStatus Status { get; set; }
    
    public decimal CalculatedInterest { get; set; }
    public decimal TotalRepayAmount { get; set; }
    public decimal MonthlyInstallment { get; set; }
}
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
    
    /// <summary>
    /// Total interest amount calculated using SI formula: (Principal × Rate × Time) / 100
    /// </summary>
    public decimal CalculatedInterest { get; set; }
    
    /// <summary>
    /// Total amount to be repaid (Principal + Interest)
    /// </summary>
    public decimal TotalRepayAmount { get; set; }
    
    /// <summary>
    /// Monthly installment amount
    /// </summary>
    public decimal MonthlyInstallment { get; set; }
}
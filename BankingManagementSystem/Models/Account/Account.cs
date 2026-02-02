namespace BankingManagementSystem.Models;

public class Account
{
    public int AccountId { get; set; }
    public AccountType AccountType { get; set; }
    public decimal Balance { get; set; }
    public AccountStatus Status { get; set; }
    public int CustomerId { get; set; }
    public DateTime CreatedDate { get; set; }

    public Account()
    {
        CreatedDate = DateTime.UtcNow;
    }
}
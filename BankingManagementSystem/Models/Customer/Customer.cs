namespace BankingManagementSystem.Models;

public class Customer
{
    public int CustomerId { get; set; }
    public DateTime BankJoiningDate { get; set; }
    public CustomerProfile Profile { get; set; } = null!;
    public bool IsClosed { get; set; } = false;

    public Customer(int customerId, CustomerProfile profile)
    {
        CustomerId = customerId;
        Profile = profile;
        BankJoiningDate = DateTime.UtcNow;
    }

    public void Close()
    {
        if (IsClosed)
            throw new InvalidOperationException("Customer is already closed");

        IsClosed = true;
    }

}
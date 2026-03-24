namespace BankingManagementSystem.Exceptions;

public class CustomerNotFoundException : BankingException
{
    public int CustomerId { get; }

    public CustomerNotFoundException(int customerId)
        : base($"Customer with ID {customerId} not found")
    {
        CustomerId = customerId;
    }
}

namespace BankingManagementSystem.Models;

public class Customer
{
    public int CustomerId { get; }
    public DateTime BankJoiningDate { get; }
    public CustomerProfile Profile { get; private set; }
    public bool IsClosed { get; private set; }

    public Customer(int customerId, CustomerProfile profile)
    {
        CustomerId = customerId;
        Profile = profile ?? throw new ArgumentNullException(nameof(profile));
        BankJoiningDate = DateTime.UtcNow;
        IsClosed = false;

        ValidateAge();
    }

    public int GetAge()
    {
        var today = DateTime.Today;
        var dob = Profile.BasicDetails.DateOfBirth;
        var age = today.Year - dob.Year;

        if (dob.Date > today.AddYears(-age))
            age--;

        return age;
    }

    private void ValidateAge()
    {
        const int minimumAge = 12;

        if (GetAge() < minimumAge)
            throw new InvalidOperationException(
                $"Customer must be at least {minimumAge} years old."
            );
    }

    public void UpdateProfile(CustomerProfile newProfile)
    {
        Profile = newProfile ?? throw new ArgumentNullException(nameof(newProfile));
    }

    public void Close()
    {
        if (IsClosed)
        {
            throw new InvalidOperationException("Customer already closed.");
        }
        IsClosed = true;
    }
}

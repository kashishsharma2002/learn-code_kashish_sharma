namespace BankingManagementSystem.Models;

public class CustomerBasicDetails
{
    public string FirstName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
    public DateTime DateOfBirth { get; init; }
    public string Nationality { get; init; } = string.Empty;
    public string Gender { get; init; } = string.Empty;
    public string MaritalStatus { get; init; } = string.Empty;
}
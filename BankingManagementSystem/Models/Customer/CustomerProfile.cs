namespace BankingManagementSystem.Models;


public class CustomerProfile
{
    public CustomerBasicDetails BasicDetails { get; set; } = new CustomerBasicDetails();
    public CustomerContactDetails ContactDetails { get; set; } = new CustomerContactDetails();
    public KycDetails KycDetails { get; set; } = new KycDetails();
}
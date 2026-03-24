namespace BankingManagementSystem.Models;


public class CustomerProfile
{
    public CustomerBasicDetails BasicDetails { get; private set; }
    public CustomerContactDetails ContactDetails { get; private set; }
    public KycDetails KycDetails { get; private set; }

    public CustomerProfile(
        CustomerBasicDetails basic,
        CustomerContactDetails contact,
        KycDetails kyc)
    {
        BasicDetails = basic ?? throw new ArgumentNullException(nameof(basic));
        ContactDetails = contact ?? throw new ArgumentNullException(nameof(contact));
        KycDetails = kyc ?? throw new ArgumentNullException(nameof(kyc));
    }
}

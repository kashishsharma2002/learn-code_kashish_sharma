namespace BankingManagementSystem.Constants;
public class LoanEligibilityCriteria
{
    public const decimal MaxPersonalLoanAmount = 50000;
    public const decimal MaxHomeLoanAmount = 50000;
    public const decimal MaxEducationLoanAmount = 100000;
    public const decimal MaxBusinessLoanAmount = 200000;
    public const decimal MaxMedicalLoanAmount = 50000;

    public const int MinEligibleAgePersonalLoan = 21;
    public const int MinEligibleAgeHomeLoan = 25;   
    public const int MinEligibleAgeEducationLoan = 17;
    public const int MinEligibleAgeBusinessLoan = 25;
    public const int MinEligibleAgeMedicalLoan = 18;

}
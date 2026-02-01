namespace BankingManagementSystem.Models;
using System;
using System.Security.Cryptography.X509Certificates;

public class CustomerProfile
{
    public CustomerBasicDetails BasicDetails { get; set; } = new CustomerBasicDetails();
    public CustomerContactDetails ContactDetails { get; set; } = new CustomerContactDetails();
    public KycDetails KycDetails { get; set; } = new KycDetails();
    public EmploymentDetails EmploymentDetails { get; set; } = new EmploymentDetails();

    
}
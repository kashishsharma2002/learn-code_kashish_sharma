namespace BankingManagementSystem.Models;
using System;
using System.ComponentModel.DataAnnotations;

public class EmploymentDetails
{
    public string EmployerName { get; init; } = string.Empty;
    public string JobTitle { get; init; } = string.Empty;
    public decimal AnnualIncome { get; init; }
    public string EmploymentType { get; init; } = string.Empty;
    public string SourceOfFunds { get; init; } = string.Empty;
}

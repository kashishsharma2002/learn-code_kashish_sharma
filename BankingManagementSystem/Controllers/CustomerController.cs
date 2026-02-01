using BankingManagementSystem.Models;
using BankingManagementSystem.Interfaces;
using BankingManagementSystem.Common;
using System.Diagnostics;
using System.Data;
using System.Security.AccessControl;
using System.ComponentModel;

namespace BankingManagementSystem.Controllers;

public class CustomerController
{
    private readonly ICustomerService _customerService;
    private readonly InputReader _inputReader;

    public CustomerController(ICustomerService customerService, InputReader inputReader)
    {
        _customerService = customerService;
        _inputReader = inputReader;
    }

    public void CreateCustomer()
    {
        var profile = ReadCustomerInput();
        var customer = _customerService.RegisterCustomer(profile);
        Console.WriteLine($"Customer created successfully with ID: {customer.CustomerId}");
    }

    private CustomerProfile ReadCustomerInput()
    {
        var basicDetails = new CustomerBasicDetails
        {
            FirstName = _inputReader.ReadRequiredString("Enter First Name:"),
            LastName = _inputReader.ReadRequiredString("Enter Last Name:"),
            DateOfBirth = _inputReader.ReadDate("Enter Date of Birth (yyyy-mm-dd):"),
            Nationality = _inputReader.ReadRequiredString("Enter Nationality:"),
            Gender = _inputReader.ReadRequiredString("Enter Gender:"),
            MaritalStatus = _inputReader.ReadRequiredString("Enter Marital Status:")
        };

        var contactDetails = new CustomerContactDetails
        {
            AddressLine1 = _inputReader.ReadRequiredString("Enter Address Line 1:"),
            AddressLine2 = _inputReader.ReadRequiredString("Enter Address Line 2:"),
            PhoneNumber = _inputReader.ReadWithRegex(
                "Enter Phone Number:",
                @"^[6-9]\d{9}$",
                "Invalid phone number"
            ),
            Email = _inputReader.ReadWithRegex(
                "Enter Email:",
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                "Invalid email"
            )
        };

        var kycDetails = new KycDetails
        {
            AadhaarNumber = _inputReader.ReadWithRegex(
                "Enter Aadhaar Number:",
                @"^\d{12}$",
                "Invalid Aadhaar number"
            ),
            PanNumber = _inputReader.ReadWithRegex(
                "Enter PAN Number:",
                @"^[A-Z]{5}[0-9]{4}[A-Z]$",
                "Invalid PAN number"
            )
        };

        return new CustomerProfile
        {
            BasicDetails = basicDetails,
            ContactDetails = contactDetails,
            KycDetails = kycDetails
        };
    }

    public void ViewCustomerById()
    {
        var customerId = _inputReader.ReadInt("Enter Customer ID:");
        if (!customerId.HasValue)
            return;

        try
        {
            var customer = _customerService.GetCustomer(customerId.Value);
            DisplayCustomer(customer);
        }
        catch (InvalidOperationException ex)
        {
            HandleCustomerError(ex.Message);
        }
        catch (Exception)
        {
            HandleCustomerError("Something went wrong. Please try again later.");
        }
    }
    public void CloseCustomer()
    {
        var customerId = ReadCustomerId();
        if (!customerId.HasValue)
            return;

        try
        {
            _customerService.CloseCustomer(customerId.Value);
            Console.WriteLine("Customer closed successfully.");
        }
        catch (InvalidOperationException ex)
        {
            HandleCustomerError(ex.Message);
        }
        catch (Exception)
        {
            HandleCustomerError("Something went wrong. Please try again later.");
        }
    }
    private int? ReadCustomerId()
    {
        var idInput = _inputReader.ReadRequiredString("Enter Customer ID to view:");
        if (!int.TryParse(idInput, out int customerId))
        {
            Console.WriteLine("Invalid ID");
            return null;
        }
        return customerId;
    }
    public void ViewAllCustomers()
    {
        try
        {
            var customers = _customerService.GetAllCustomers();
            foreach (var customer in customers)
            {
                Console.WriteLine("-----------------------");
                DisplayCustomer(customer);
                Console.WriteLine("-----------------------");
            }
        }
        catch (Exception ex)
        {
            HandleCustomerError(ex.Message);
        }
    }
    private void DisplayCustomer(Customer customer)
    {
        Console.WriteLine($"Customer ID: {customer.CustomerId}");
        Console.WriteLine($"Name: {customer.Profile.BasicDetails.FirstName} {customer.Profile.BasicDetails.LastName}");
        Console.WriteLine($"Date of Birth: {customer.Profile.BasicDetails.DateOfBirth:yyyy-MM-dd}");
        Console.WriteLine($"Nationality: {customer.Profile.BasicDetails.Nationality}");
        Console.WriteLine($"Email: {customer.Profile.ContactDetails.Email}");
        Console.WriteLine($"Joined On: {customer.BankJoiningDate}");
    }
    private void HandleCustomerError(string message)
    {
        Console.WriteLine($"Error: {message}");
    }
}
using BankingManagementSystem.Models;
using BankingManagementSystem.Interfaces;
using BankingManagementSystem.Common;
using BankingManagementSystem.Exceptions;

namespace BankingManagementSystem.Controllers;

public class CustomerController
{
    private readonly ICustomerService _customerService;
    private readonly IInputReader _inputReader;

    public CustomerController(ICustomerService customerService, IInputReader inputReader)
    {
        _customerService = customerService;
        _inputReader = inputReader;
    }

    public void CreateCustomer()
    {
        try
        {
            var profile = ReadCustomerInput();

            var customer = _customerService.RegisterCustomer(profile);

            Console.WriteLine($"Customer created successfully with ID: {customer.CustomerId}");
        }
        catch (InvalidBankingOperationException ex)
        {
            Console.WriteLine($"Validation Error: {ex.Message}");
        }
        catch (ArgumentNullException ex)
        {
            Console.WriteLine($"Input Error: {ex.Message}");
        }
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
            City = _inputReader.ReadRequiredString("Enter City:"),
            State = _inputReader.ReadRequiredString("Enter State:"),
            PostalCode = _inputReader.ReadRequiredString("Enter Postal Code:"),
            Country = _inputReader.ReadRequiredString("Enter Country:"),
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

        return new CustomerProfile(basicDetails, contactDetails, kycDetails);

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
        catch (CustomerNotFoundException ex)
        {
            Console.WriteLine($"Not Found: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    public void ViewAllCustomers()
    {
        try
        {
            var customers = _customerService.GetAllCustomers();

            foreach (var customer in customers)
            {
                DisplayCustomer(customer);
            }
        }
        catch (InvalidBankingOperationException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    private void DisplayCustomer(Customer customer)
    {
        Console.WriteLine("-----------------------");
        Console.WriteLine($"Customer ID: {customer.CustomerId}");
        Console.WriteLine($"Name: {customer.Profile.BasicDetails.FirstName} {customer.Profile.BasicDetails.LastName}");
        Console.WriteLine($"Date of Birth: {customer.Profile.BasicDetails.DateOfBirth:yyyy-MM-dd}");
        Console.WriteLine($"Nationality: {customer.Profile.BasicDetails.Nationality}");
        Console.WriteLine($"Email: {customer.Profile.ContactDetails.Email}");
        Console.WriteLine($"Joined On: {customer.BankJoiningDate:yyyy-MM-dd}");
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
        catch (CustomerNotFoundException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        catch (InvalidBankingOperationException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    private int? ReadCustomerId()
    {
        var idInput = _inputReader.ReadRequiredString("Enter Customer ID:");

        if (!int.TryParse(idInput, out int customerId))
        {
            Console.WriteLine("Invalid ID format");
            return null;
        }
        return customerId;
    }
}
using BankingManagementSystem.Models;
using BankingManagementSystem.Interfaces;
using Microsoft.VisualBasic;
using System.ComponentModel.DataAnnotations;

namespace BankingManagementSystem.Repositories;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;
    private static int _customerIdCounter = 1;

    public CustomerService(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public Customer RegisterCustomer(CustomerProfile profile)
    {
        if (profile == null)
            throw new ArgumentException("Customer profile cannot be null");

        ValidateAge(profile.BasicDetails.DateOfBirth);

        var customer = new Customer(_customerIdCounter++, profile);
        _customerRepository.Add(customer);

        return customer;
    }

    public Customer GetCustomer(int customerId)
    {
        var customer = _customerRepository.GetById(customerId);
        if (customer == null)
            throw new InvalidOperationException("Customer not found");

        return customer;
    }

    public IEnumerable<Customer> GetAllCustomers()
    {
        var customerList = _customerRepository.GetAll();
        if(!customerList.Any())
            throw new InvalidOperationException("No customers found");
        return customerList;
    }

    public void CloseCustomer(int customerId)
    {
        var customer = _customerRepository.GetById(customerId);
        if (customer == null)
            throw new InvalidOperationException("Customer not found");
        
        if (customer.IsClosed)
            throw new InvalidOperationException("Customer is already closed");

        customer.Close();
    }

    private void ValidateAge(DateTime dateOfBirth)
    {
        var today = DateTime.Today;
        var age = today.Year - dateOfBirth.Year;

        if (dateOfBirth.Date > today.AddYears(-age))
            age--;

        if (age < 12)
            throw new InvalidOperationException("Customer must be at least 12 years old to open a bank account");
    }
}
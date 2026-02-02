using BankingManagementSystem.Models;
using BankingManagementSystem.Interfaces;
using BankingManagementSystem.Exceptions;

namespace BankingManagementSystem.Services;

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
            throw new ArgumentNullException(nameof(profile), "Customer profile cannot be null");

        ValidateAge(profile.BasicDetails.DateOfBirth);

        var customer = new Customer(_customerIdCounter++, profile);
        _customerRepository.Add(customer);

        return customer;
    }

    public Customer GetCustomer(int customerId)
    {
        var customer = _customerRepository.GetById(customerId);
        if (customer == null)
            throw new CustomerNotFoundException(customerId);

        return customer;
    }

    public IEnumerable<Customer> GetAllCustomers()
    {
        var customerList = _customerRepository.GetAll();
        if (!customerList.Any())
            throw new InvalidBankingOperationException("No customers found");
        return customerList;
    }

    public void CloseCustomer(int customerId)
    {
        var customer = _customerRepository.GetById(customerId);
        if (customer == null)
            throw new CustomerNotFoundException(customerId);
        
        if (customer.IsClosed)
            throw new InvalidBankingOperationException("Customer is already closed");

        customer.Close();
        _customerRepository.Update(customer);
    }

    public void UpdateCustomer(int customerId, CustomerProfile profile)
    {
        if (profile == null)
            throw new ArgumentNullException(nameof(profile));

        var customer = _customerRepository.GetById(customerId);
        if (customer == null)
            throw new CustomerNotFoundException(customerId);

        customer.Profile = profile;
        _customerRepository.Update(customer);
    }

    private void ValidateAge(DateTime dateOfBirth)
    {
        var today = DateTime.Today;
        var age = today.Year - dateOfBirth.Year;

        if (dateOfBirth.Date > today.AddYears(-age))
            age--;

        const int minimumAge = 12;
        if (age < minimumAge)
            throw new InvalidOperationException($"Customer must be at least {minimumAge} years old to open a bank account");
    }
}
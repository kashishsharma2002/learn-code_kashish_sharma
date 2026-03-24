using BankingManagementSystem.Models;
using BankingManagementSystem.Interfaces;

namespace BankingManagementSystem.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly Dictionary<int, Customer> _customers = new();

    public void Add(Customer customer)
    {
        _customers[customer.CustomerId] = customer;
    }

    public Customer? GetById(int customerId)
    {
        _customers.TryGetValue(customerId, out var customer);
        
        return customer;
    }

    public IEnumerable<Customer> GetAll()
    {
        return _customers.Values;
    }

    public void Update(Customer customer)
    {
        if (customer == null)
            throw new ArgumentNullException(nameof(customer));

        if (_customers.ContainsKey(customer.CustomerId))
            _customers[customer.CustomerId] = customer;
        else
            throw new KeyNotFoundException($"Customer {customer.CustomerId} not found");
    }
}
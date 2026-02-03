using BankingManagementSystem.Models;

namespace BankingManagementSystem.Interfaces;

public interface ICustomerRepository
{
    void Add(Customer customer);
    Customer? GetById(int customerId);
    IEnumerable<Customer> GetAll();
    void Update(Customer customer);
}
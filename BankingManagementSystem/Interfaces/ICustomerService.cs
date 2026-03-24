using BankingManagementSystem.Models;

namespace BankingManagementSystem.Interfaces;

public interface ICustomerService
{
    Customer RegisterCustomer(CustomerProfile profile);
    Customer GetCustomer(int customerId);
    IEnumerable<Customer> GetAllCustomers();
    void CloseCustomer(int customerId);
    void UpdateCustomer(int customerId, CustomerProfile profile);
}
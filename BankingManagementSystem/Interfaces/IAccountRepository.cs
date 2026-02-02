using BankingManagementSystem.Models;
namespace BankingManagementSystem.Interfaces;

public interface IAccountRepository
{
    void Add(Account account);
    Account GetById(int accountId);
    IEnumerable<Account> GetAll();
    void Remove(int accountId);
    Account GetByCustomerId(int customerId);
    void Update(Account account);
    bool AccountExists(int accountId);
}
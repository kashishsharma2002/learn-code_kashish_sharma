using BankingManagementSystem.Models;
namespace BankingManagementSystem.Interfaces;

public interface IAccountRepository
{
    void Add(Account account);
    void Update(Account account);
    Account? GetById(int accountId);
    IEnumerable<Account> GetAll();
    
}
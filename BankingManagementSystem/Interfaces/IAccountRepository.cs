using BankingManagementSystem.Models;
namespace BankingManagementSystem.Interfaces;

public interface IAccountRepository
{
    void Add(Account account);
    Account? GetById(int accountId);
    IEnumerable<Account> GetAll();
    void Update(Account account);
}
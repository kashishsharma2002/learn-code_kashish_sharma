using BankingManagementSystem.Models;

namespace BankingManagementSystem.Interfaces;

public interface IAccountService
{
    Account CreateAccount(Account accountInfo);
    Account GetAccount(int accountId);
    IEnumerable<Account> GetAllAccounts();
    void CloseAccount(int accountId);
}
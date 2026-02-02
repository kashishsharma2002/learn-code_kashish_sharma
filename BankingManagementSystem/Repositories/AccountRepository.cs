using BankingManagementSystem.Models;
using BankingManagementSystem.Interfaces;
namespace BankingManagementSystem.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly Dictionary<int, Account> _accounts = new();
    private int _nextAccountId = 1;

    public void Add(Account account)
    {
        account.AccountId = _nextAccountId++;
        _accounts[account.AccountId] = account;
    }

    public Account? GetById(int accountId)
    {
        if (_accounts.TryGetValue(accountId, out var account))
            return account;
        return null;
    }
    public IEnumerable<Account> GetAll()
    {
        return _accounts.Values;
    }

    public void Remove(int accountId)
    {
        _accounts.Remove(accountId);
    }

    public Account? GetByCustomerId(int customerId)
    {
        return _accounts.Values.FirstOrDefault(a => a.CustomerId == customerId);
    }

    public void Update(Account account)
    {
        if (_accounts.ContainsKey(account.AccountId))
        {
            _accounts[account.AccountId] = account;
        }
        else
        {
            throw new ArgumentException("Account not found");
        }
    }

    public bool AccountExists(int accountId)
    {
        return _accounts.ContainsKey(accountId);
    }
}
using BankingManagementSystem.Models;
using BankingManagementSystem.Interfaces;
using BankingManagementSystem.Constants;
using BankingManagementSystem.Exceptions;

namespace BankingManagementSystem.Services;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;
    private readonly ICustomerRepository _customerRepository;

    public AccountService(IAccountRepository accountRepository, ICustomerRepository customerRepository)
    {
        _accountRepository = accountRepository;
        _customerRepository = customerRepository;
    }

    public Account CreateAccount(Account accountInfo)
    {
        ArgumentNullException.ThrowIfNull(accountInfo, nameof(accountInfo));

        var customer = _customerRepository.GetById(accountInfo.CustomerId);

        if (customer == null)
            throw new CustomerNotFoundException(accountInfo.CustomerId);

        ValidateMinimumDeposit(accountInfo.AccountType, accountInfo.Balance);

        var account = new Account
        {
            CustomerId = accountInfo.CustomerId,
            AccountType = accountInfo.AccountType,
            Balance = accountInfo.Balance,
            Status = AccountStatus.Active,
            CreatedDate = DateTime.UtcNow
        };

        _accountRepository.Add(account);
        
        return account;
    }

    public Account GetAccount(int accountId)
    {
        var account = _accountRepository.GetById(accountId);

        if (account == null)
            throw new AccountNotFoundException(accountId);

        return account;
    }

    public IEnumerable<Account> GetAllAccounts()
    {
        var accountList = _accountRepository.GetAll();

        if (!accountList.Any())
            throw new InvalidBankingOperationException("No accounts found");

        return accountList;
    }

    public void CloseAccount(int accountId)
    {
        var account = _accountRepository.GetById(accountId);

        if (account == null)
            throw new AccountNotFoundException(accountId);

        if (account.Status == AccountStatus.Closed)
            throw new InvalidBankingOperationException("Account is already closed");

        account.Status = AccountStatus.Closed;

        _accountRepository.Update(account);
    }

    private void ValidateMinimumDeposit(AccountType accountType, decimal initialDeposit)
    {
        decimal minimumDeposit = accountType switch
        {
            AccountType.Savings => AccountMinimumDeposit.SavingsAccountMinDeposit,
            AccountType.Checking => AccountMinimumDeposit.CheckingAccountMinDeposit,
            AccountType.Business => AccountMinimumDeposit.BusinessAccountMinDeposit,
            _ => throw new ArgumentException("Invalid account type")
        };

        if (initialDeposit < minimumDeposit)
            throw new InvalidDepositAmountException(minimumDeposit, initialDeposit, accountType.ToString());
    }

}
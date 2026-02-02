using BankingManagementSystem.Models;
using BankingManagementSystem.Interfaces;
using BankingManagementSystem.Common;
using BankingManagementSystem.Exceptions;

namespace BankingManagementSystem.Controllers;

public class AccountController
{
    private readonly IAccountService _accountService;
    private readonly IInputReader _inputReader;

    public AccountController(IAccountService accountService, IInputReader inputReader)
    {
        _accountService = accountService;
        _inputReader = inputReader;
    }

    public void CreateCustomerAccount()
    {
        try
        {
            var account = ReadAccountFromInput();
            var createdAccount = _accountService.CreateAccount(account);
            Console.WriteLine($"Account created successfully with ID: {createdAccount.AccountId}");
        }
        catch (CustomerNotFoundException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        catch (InvalidDepositAmountException ex)
        {
            Console.WriteLine($"Deposit Error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error creating account: {ex.Message}");
        }
    }

    private Account ReadAccountFromInput()
    {
        var customerId = _inputReader.ReadInt("Enter Customer ID:");
        if (!customerId.HasValue)
            throw new ArgumentException("Customer ID is required");
            
        var accountType = ReadAccountType("Enter Account Type (Savings/Checking/Business):");
        var initialDeposit = _inputReader.ReadDecimal("Enter Initial Deposit Amount:");

        return new Account
        {
            CustomerId = customerId.Value,
            AccountType = accountType,
            Balance = initialDeposit,
            Status = AccountStatus.Active
        };
    }

    private AccountType ReadAccountType(string prompt)
    {
        while (true)
        {
            var input = _inputReader.ReadRequiredString(prompt);
            if (Enum.TryParse<AccountType>(input, true, out var accountType))
                return accountType;

            Console.WriteLine("Invalid account type. Please enter Savings, Checking, or Business.");
        }
    }

    public Account GetAccountDetails()
    {
        var accountId = _inputReader.ReadInt("Enter Account ID:");
        if (!accountId.HasValue)
            throw new ArgumentException("Invalid Account ID");

        try
        {
            var account = _accountService.GetAccount(accountId.Value);
            DisplayAccount(account);
            return account;
        }
        catch (AccountNotFoundException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return null;
        }
    }

    private void DisplayAccount(Account account)
    {
        Console.WriteLine($"Account ID: {account.AccountId}");
        Console.WriteLine($"Customer ID: {account.CustomerId}");
        Console.WriteLine($"Account Type: {account.AccountType}");
        Console.WriteLine($"Balance: {account.Balance:C}");
        Console.WriteLine($"Status: {account.Status}");
    }

    public IEnumerable<Account> GetAllAccountsDetails()
    {
        try
        {
            var accounts = _accountService.GetAllAccounts();
            foreach (var account in accounts)
            {
                DisplayAccount(account);
                Console.WriteLine("---");
            }
            return accounts;
        }
        catch (InvalidBankingOperationException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return Enumerable.Empty<Account>();
        }
    }

    public void CloseCustomerAccount()
    {
        var accountId = _inputReader.ReadInt("Enter Account ID to close:");
        if (!accountId.HasValue)
            return;

        try
        {
            _accountService.CloseAccount(accountId.Value);
            Console.WriteLine("Account closed successfully.");
        }
        catch (AccountNotFoundException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
        catch (InvalidBankingOperationException ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
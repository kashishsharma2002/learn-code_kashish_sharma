using BankingManagementSystem.Controllers;
using BankingManagementSystem.Repositories;
using BankingManagementSystem.Services;
using BankingManagementSystem.Interfaces;
using BankingManagementSystem.Models;
using BankingManagementSystem.Common;

namespace BankingManagementSystem;

public class Program
{
    private static CustomerController _customerController = null!;
    private static AccountController _accountController = null!;
    private static TransactionController _transactionController = null!;
    private static LoanController _loanController = null!;

    public static void Main(string[] args)
    {
        ConfigureDependencies();
        ShowMainMenu();
    }

    private static void ConfigureDependencies()
    {
        var inputReader = new InputReader();

        var customerRepository = new CustomerRepository();
        var accountRepository = new AccountRepository();
        var transactionRepository = new TransactionRepository();
        var loanRepository = new LoanRepository();

        var customerService = new CustomerService(customerRepository);
        var accountService = new AccountService(accountRepository, customerRepository);
        var transactionService = new TransactionService(transactionRepository, accountRepository);
        var loanService = new LoanService(loanRepository, customerRepository);

        _customerController = new CustomerController(customerService, inputReader);
        _accountController = new AccountController(accountService, inputReader);
        _transactionController = new TransactionController(transactionService, inputReader);
        _loanController = new LoanController(loanService, inputReader);
    }

    private static void ShowMainMenu()
    {
        while (true)
        {
            Console.WriteLine("\n=== BANKING MANAGEMENT SYSTEM ===");
            Console.WriteLine("1. Customer Management");
            Console.WriteLine("2. Account Management");
            Console.WriteLine("3. Transactions");
            Console.WriteLine("4. Loans");
            Console.WriteLine("5. Exit");

            Console.Write("Choose option: ");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    HandleCustomerMenu();
                    break;
                case "2":
                    HandleAccountMenu();
                    break;
                case "3":
                    HandleTransactionMenu();
                    break;
                case "4":
                    HandleLoanMenu();
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
        }
    }

    private static void HandleCustomerMenu()
    {
        Console.WriteLine("\n--- CUSTOMER MANAGEMENT ---");
        Console.WriteLine("1. Create Customer");
        Console.WriteLine("2. View Customer by ID");
        Console.WriteLine("3. View All Customers");
        Console.WriteLine("4. Close Customer");
        Console.Write("Choose option: ");
        var choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                _customerController.CreateCustomer();
                break;
            case "2":
                _customerController.ViewCustomerById();
                break;
            case "3":
                _customerController.ViewAllCustomers();
                break;
            case "4":
                _customerController.CloseCustomer();
                break;
            default:
                Console.WriteLine("Invalid option.");
                break;
        }
    }

    private static void HandleAccountMenu()
    {
        Console.WriteLine("\n--- ACCOUNT MANAGEMENT ---");
        Console.WriteLine("1. Create Customer Account");
        Console.WriteLine("2. View Customer Accounts");
        Console.WriteLine("3. View All Accounts");
        Console.WriteLine("4. Close Account");
        Console.Write("Choose option: ");
        var choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                _accountController.CreateCustomerAccount();
                break;
            case "2":
                _accountController.GetAccountDetails();
                break;
            case "3":
                _accountController.GetAllAccountsDetails();
                break;
            case "4":
                _accountController.CloseCustomerAccount();
                break;
            default:
                Console.WriteLine("Invalid option.");
                break;
        }
    }

    private static void HandleTransactionMenu()
    {
        Console.WriteLine("\n--- TRANSACTIONS ---");
        Console.WriteLine("1. Process Transaction");
        Console.WriteLine("2. View All Transactions");
        Console.Write("Choose option: ");
        var choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                _transactionController.ProcessTransaction();
                break;
            case "2":
                _transactionController.ViewAllTransactions();
                break;
            default:
                Console.WriteLine("Invalid option.");
                break;
        }
    }


    private static void HandleLoanMenu()
    {
        Console.WriteLine("\n--- LOANS ---");
        Console.WriteLine("1. Apply for Loan");
        Console.WriteLine("2. View Loan Details by ID");
        Console.Write("Choose option: ");
        var choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                _loanController.ApplyLoanApplication();
                break;
            case "2":
                _loanController.ViewLoanById();
                break;
            default:
                Console.WriteLine("Invalid option.");
                break;
        }
    }
}
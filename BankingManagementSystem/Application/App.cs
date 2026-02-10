using BankingManagementSystem.Controllers;
using BankingManagementSystem.Repositories;
using BankingManagementSystem.Services;
using BankingManagementSystem.Common;
using BankingManagementSystem.UserInterface;

namespace BankingManagementSystem.Application;

public class App
{
    private readonly MainMenu _mainMenu;

    public App()
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

        var customerController = new CustomerController(customerService, inputReader);
        var accountController = new AccountController(accountService, inputReader);
        var transactionController = new TransactionController(transactionService, inputReader);
        var loanController = new LoanController(loanService, inputReader);

        var menuContext = new MenuContext(
            customerController,
            accountController,
            transactionController,
            loanController
        );

        _mainMenu = new MainMenu(menuContext);
    }

    public void Run()
    {
        _mainMenu.Show();
    }
}

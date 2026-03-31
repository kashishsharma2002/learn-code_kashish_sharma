using FinanceTracker.ConsoleClient.Abstractions;
using FinanceTracker.ConsoleClient.Operations;

namespace FinanceTracker.ConsoleClient.Menu;

public class MenuService : IMenuService
{
    private readonly IConsoleService _consoleService;
    private readonly IUserSession _userSession;
    private readonly UserOperationHandler _userHandler;
    private readonly TransactionOperationHandler _transactionHandler;
    private readonly BudgetOperationHandler _budgetHandler;
    private readonly ReportOperationHandler _reportHandler;

    public MenuService(
        IConsoleService consoleService,
        IUserSession userSession,
        IApiClient apiClient)
    {
        _consoleService = consoleService;
        _userSession = userSession;
        _userHandler = new UserOperationHandler(apiClient, consoleService, userSession);
        _transactionHandler = new TransactionOperationHandler(apiClient, consoleService, userSession);
        _budgetHandler = new BudgetOperationHandler(apiClient, consoleService, userSession);
        _reportHandler = new ReportOperationHandler(apiClient, consoleService, userSession);
    }

    public async Task RunAsync()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        bool isRunning = true;

        while (isRunning)
        {
            if (_userSession.IsLoggedIn)
                isRunning = await DisplayUserMenuAsync();
            else
                isRunning = await DisplayMainMenuAsync();
        }

        _consoleService.WriteLine("\nThank you for using Finance Tracker. Goodbye!");
    }

    private async Task<bool> DisplayMainMenuAsync()
    {
        PrintHeader();
        _consoleService.WriteLine("SELECT AN OPTION:");
        _consoleService.WriteLine("  1. Create New User");
        _consoleService.WriteLine("  2. Login to Existing User");
        _consoleService.WriteLine("  3. Exit Application");
        _consoleService.WriteLine("\n Enter your choice (1-3): ");

        string? choice = _consoleService.ReadLine();

        return choice switch
        {
            "1" => await HandleCreateUserAsync(),
            "2" => await HandleLoginUserAsync(),
            "3" => false,
            _   => HandleInvalidChoice()
        };
    }

    private async Task<bool> DisplayUserMenuAsync()
    {
        PrintHeader();
        _consoleService.WriteLine("SELECT AN OPTION:");
        _consoleService.WriteLine("  1. Add Transaction");
        _consoleService.WriteLine("  2. View Transactions");
        _consoleService.WriteLine("  3. Set Monthly Budget");
        _consoleService.WriteLine("  4. View Budgets");
        _consoleService.WriteLine("  5. View Monthly Summary");
        _consoleService.WriteLine("  6. Delete Transaction");
        _consoleService.WriteLine("  7. Logout");
        _consoleService.WriteLine("  8. Exit Application");
        _consoleService.WriteLine("\n Enter your choice (1-8): ");

        string? choice = _consoleService.ReadLine();

        return choice switch
        {
            "1" => await HandleAddTransactionAsync(),
            "2" => await HandleViewTransactionsAsync(),
            "3" => await HandleSetBudgetAsync(),
            "4" => await HandleViewBudgetsAsync(),
            "5" => await HandleViewSummaryAsync(),
            "6" => await HandleDeleteTransactionAsync(),
            "7" => HandleLogout(),
            "8" => false,
            _   => HandleInvalidChoice()
        };
    }

    private void PrintHeader()
    {
        _consoleService.Clear();
        _consoleService.WriteLine("FINANCE TRACKER CONSOLE");

        if (_userSession.IsLoggedIn)
            _consoleService.WriteLine($"\nCurrent User: {_userSession.CurrentUserName}");

        _consoleService.WriteLine(string.Empty);
    }

    private async Task<bool> HandleCreateUserAsync()
    {
        await _userHandler.CreateNewUserAsync();
        return true;
    }

    private async Task<bool> HandleLoginUserAsync()
    {
        await _userHandler.LoginToExistingUserAsync();
        return true;
    }

    private bool HandleLogout()
    {
        _userHandler.Logout();
        return true;
    }

    private async Task<bool> HandleAddTransactionAsync()
    {
        await _transactionHandler.AddTransactionAsync();
        return true;
    }

    private async Task<bool> HandleViewTransactionsAsync()
    {
        await _transactionHandler.ViewTransactionsAsync();
        return true;
    }

    private async Task<bool> HandleDeleteTransactionAsync()
    {
        await _transactionHandler.DeleteTransactionAsync();
        return true;
    }

    private async Task<bool> HandleSetBudgetAsync()
    {
        await _budgetHandler.SetBudgetAsync();
        return true;
    }

    private async Task<bool> HandleViewBudgetsAsync()
    {
        await _budgetHandler.ViewBudgetsAsync();
        return true;
    }

    private async Task<bool> HandleViewSummaryAsync()
    {
        await _reportHandler.ViewMonthlySummaryAsync();
        return true;
    }

    private bool HandleInvalidChoice()
    {
        _consoleService.WriteError("Invalid choice. Please try again.");
        _consoleService.WaitForKeyPress();
        return true;
    }
}

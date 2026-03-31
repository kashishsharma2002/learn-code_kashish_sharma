using FinanceTracker.ConsoleClient.Abstractions;
using FinanceTracker.ConsoleClient.Models;

namespace FinanceTracker.ConsoleClient.Operations;

public class TransactionOperationHandler
{
    private readonly IApiClient _apiClient;
    private readonly IConsoleService _consoleService;
    private readonly IUserSession _userSession;

    public TransactionOperationHandler(IApiClient apiClient, IConsoleService consoleService, IUserSession userSession)
    {
        _apiClient = apiClient;
        _consoleService = consoleService;
        _userSession = userSession;
    }

    public async Task AddTransactionAsync()
    {
        _consoleService.Clear();
        _consoleService.WriteLine("ADD NEW TRANSACTION");

        if (!GetTransactionType(out int type)) return;
        if (!GetAmount(out decimal amount)) return;
        if (!GetCategory(out int category)) return;
        GetDateAndDescription(out DateTime date, out string description);

        var request = new AddTransactionRequest
        {
            UserId = _userSession.CurrentUserId,
            Type = type,
            Amount = amount,
            Category = category,
            Date = date,
            Description = description
        };

        var transaction = await _apiClient.AddTransactionAsync(request);
        if (transaction != null)
        {
            _consoleService.WriteSuccess($"Transaction added successfully! ID: {transaction.Id}");

            if (!string.IsNullOrEmpty(transaction.AlertMessage))
            {
                _consoleService.WriteLine("");
                _consoleService.WriteWarning(transaction.AlertMessage);
            }
        }

        _consoleService.WaitForKeyPress();
    }

    public async Task ViewTransactionsAsync()
    {
        _consoleService.Clear();
        _consoleService.WriteLine("VIEW TRANSACTIONS");

        _consoleService.WriteLine("Filter Options:");
        _consoleService.WriteLine("  1. View all transactions");
        _consoleService.WriteLine("  2. Filter by date range");
        _consoleService.WriteLine("  3. Filter by category");
        _consoleService.WriteLine("\n Enter option (1-3): ");

        string? option = _consoleService.ReadLine();

        var transactions = option switch
        {
            "1" => await _apiClient.GetTransactionsAsync(new TransactionsQuery { UserId = _userSession.CurrentUserId }),
            "2" => await GetTransactionsByDateAsync(),
            "3" => await GetTransactionsByCategoryAsync(),
            _   => null
        };

        if (transactions?.Count > 0)
            DisplayTransactionTable(transactions);
        else
            _consoleService.WriteWarning("No transactions found.");

        _consoleService.WaitForKeyPress();
    }

    public async Task DeleteTransactionAsync()
    {
        _consoleService.Clear();
        _consoleService.WriteLine("DELETE TRANSACTION");

        var transactions = await _apiClient.GetTransactionsAsync(new TransactionsQuery { UserId = _userSession.CurrentUserId });
        if (transactions?.Count == 0)
        {
            _consoleService.WriteWarning("No transactions to delete.");
            _consoleService.WaitForKeyPress();
            return;
        }

        DisplayTransactionTable(transactions!);

        _consoleService.WriteLine("\nEnter transaction ID to delete (GUID): ");
        string? idInput = _consoleService.ReadLine();

        if (!Guid.TryParse(idInput, out Guid transactionId))
        {
            _consoleService.WriteError("Invalid GUID format.");
            _consoleService.WaitForKeyPress();
            return;
        }

        bool deleted = await _apiClient.DeleteTransactionAsync(transactionId);
        if (deleted)
            _consoleService.WriteSuccess("Transaction deleted successfully!");
        else
            _consoleService.WriteError("Failed to delete transaction.");

        _consoleService.WaitForKeyPress();
    }

    private bool GetTransactionType(out int type)
    {
        _consoleService.WriteLine("\nTransaction Type:");
        _consoleService.WriteLine("  0. Income");
        _consoleService.WriteLine("  1. Expense");
        _consoleService.WriteLine("Enter type (0-1): ");

        if (!int.TryParse(_consoleService.ReadLine(), out type) || (type != 0 && type != 1))
        {
            _consoleService.WriteError("Invalid type.");
            _consoleService.WaitForKeyPress();
            return false;
        }
        return true;
    }

    private bool GetAmount(out decimal amount)
    {
        _consoleService.WriteLine("\nEnter amount: ");
        if (!decimal.TryParse(_consoleService.ReadLine(), out amount) || amount <= 0)
        {
            _consoleService.WriteError("Invalid amount.");
            _consoleService.WaitForKeyPress();
            return false;
        }
        return true;
    }

    private bool GetCategory(out int category)
    {
        _consoleService.WriteLine("\nCategory:");
        DisplayCategories();
        _consoleService.WriteLine("Enter category (0-6): ");

        if (!int.TryParse(_consoleService.ReadLine(), out category) || category < 0 || category > 6)
        {
            _consoleService.WriteError("Invalid category.");
            _consoleService.WaitForKeyPress();
            return false;
        }
        return true;
    }

    private void GetDateAndDescription(out DateTime date, out string description)
    {
        _consoleService.WriteLine("\nEnter date (yyyy-MM-dd) [press Enter for today]: ");
        string? dateInput = _consoleService.ReadLine();
        date = string.IsNullOrWhiteSpace(dateInput) ? DateTime.Now : DateTime.Parse(dateInput);

        _consoleService.WriteLine("Enter description: ");
        description = _consoleService.ReadLine() ?? string.Empty;
    }

    private async Task<List<TransactionResponse>?> GetTransactionsByDateAsync()
    {
        _consoleService.WriteLine("Enter start date (yyyy-MM-dd): ");
        if (!DateTime.TryParse(_consoleService.ReadLine(), out DateTime startDate))
        {
            _consoleService.WriteError("Invalid date format.");
            return null;
        }

        _consoleService.WriteLine("Enter end date (yyyy-MM-dd): ");
        if (!DateTime.TryParse(_consoleService.ReadLine(), out DateTime endDate))
        {
            _consoleService.WriteError("Invalid date format.");
            return null;
        }

        var query = new TransactionsQuery
        {
            UserId = _userSession.CurrentUserId,
            StartDate = startDate,
            EndDate = endDate
        };
        return await _apiClient.GetTransactionsAsync(query);
    }

    private async Task<List<TransactionResponse>?> GetTransactionsByCategoryAsync()
    {
        _consoleService.WriteLine("\nCategory:");
        DisplayCategories();
        _consoleService.WriteLine("Enter category (0-6): ");

        if (!int.TryParse(_consoleService.ReadLine(), out int category) || category < 0 || category > 6)
        {
            _consoleService.WriteError("Invalid category.");
            return null;
        }

        var query = new TransactionsQuery
        {
            UserId = _userSession.CurrentUserId,
            Category = category
        };
        return await _apiClient.GetTransactionsAsync(query);
    }

    private void DisplayTransactionTable(List<TransactionResponse> transactions)
    {
        _consoleService.WriteLine("| ID (first 8) | Type    | Amount    | Category       | Date       |");

        foreach (var t in transactions)
        {
            string typeStr = t.Type == 0 ? "Income" : "Expense";
            string categoryStr = GetCategoryName(t.Category);
            string amountStr = t.Type == 0 ? $"+{t.Amount:F2}" : $"-{t.Amount:F2}";
            _consoleService.WriteLine($"| {t.Id.ToString()[..8],-12} | {typeStr,-7} | {amountStr,9} | {categoryStr,-14} | {t.Date:yyyy-MM-dd} |");
        }
    }

    private void DisplayCategories()
    {
        _consoleService.WriteLine("  0. Groceries");
        _consoleService.WriteLine("  1. Salary");
        _consoleService.WriteLine("  2. Utilities");
        _consoleService.WriteLine("  3. Entertainment");
        _consoleService.WriteLine("  4. Transportation");
        _consoleService.WriteLine("  5. Healthcare");
        _consoleService.WriteLine("  6. Other");
    }

    private string GetCategoryName(int category) => category switch
    {
        0 => "Groceries",
        1 => "Salary",
        2 => "Utilities",
        3 => "Entertainment",
        4 => "Transportation",
        5 => "Healthcare",
        6 => "Other",
        _ => "Unknown"
    };
}

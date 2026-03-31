using FinanceTracker.ConsoleClient.Abstractions;
using FinanceTracker.ConsoleClient.Models;

namespace FinanceTracker.ConsoleClient.Operations;

public class BudgetOperationHandler
{
    private readonly IApiClient _apiClient;
    private readonly IConsoleService _consoleService;
    private readonly IUserSession _userSession;

    public BudgetOperationHandler(IApiClient apiClient, IConsoleService consoleService, IUserSession userSession)
    {
        _apiClient = apiClient;
        _consoleService = consoleService;
        _userSession = userSession;
    }

    public async Task SetBudgetAsync()
    {
        _consoleService.Clear();
        _consoleService.WriteLine("SET MONTHLY BUDGET");

        if (!GetCategory(out int category)) return;
        if (!GetMonthlyLimit(out decimal monthlyLimit)) return;
        GetMonthAndYear(out int month, out int year);

        var request = new SetBudgetRequest
        {
            UserId = _userSession.CurrentUserId,
            Category = category,
            MonthlyLimit = monthlyLimit,
            Month = month,
            Year = year
        };

        var budget = await _apiClient.SetBudgetAsync(request);
        if (budget != null)
            _consoleService.WriteSuccess("Budget set successfully!");

        _consoleService.WaitForKeyPress();
    }

    public async Task ViewBudgetsAsync()
    {
        _consoleService.Clear();
        _consoleService.WriteLine("VIEW BUDGETS");

        var budgets = await _apiClient.GetBudgetsAsync();
        if (budgets?.Count > 0)
        {
            var userBudgets = budgets.Where(b => b.UserId == _userSession.CurrentUserId).ToList();
            if (userBudgets.Count > 0)
                DisplayBudgetTable(userBudgets);
            else
                _consoleService.WriteWarning("No budgets set for this user.");
        }
        else
        {
            _consoleService.WriteWarning("No budgets available.");
        }

        _consoleService.WaitForKeyPress();
    }

    private bool GetCategory(out int category)
    {
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

    private bool GetMonthlyLimit(out decimal monthlyLimit)
    {
        _consoleService.WriteLine("\nEnter monthly limit: ");
        if (!decimal.TryParse(_consoleService.ReadLine(), out monthlyLimit) || monthlyLimit <= 0)
        {
            _consoleService.WriteError("Invalid amount.");
            _consoleService.WaitForKeyPress();
            return false;
        }
        return true;
    }

    private void GetMonthAndYear(out int month, out int year)
    {
        month = DateTime.Now.Month;
        year = DateTime.Now.Year;

        _consoleService.WriteLine("Enter month (1-12) [press Enter for current]: ");
        string? monthInput = _consoleService.ReadLine();
        if (!string.IsNullOrWhiteSpace(monthInput))
        {
            if (int.TryParse(monthInput, out int parsedMonth) && parsedMonth >= 1 && parsedMonth <= 12)
            {
                month = parsedMonth;
            }
            else
            {
                _consoleService.WriteWarning("Invalid month. Using current month.");
            }
        }

        _consoleService.WriteLine("Enter year [press Enter for current]: ");
        string? yearInput = _consoleService.ReadLine();
        if (!string.IsNullOrWhiteSpace(yearInput))
        {
            if (int.TryParse(yearInput, out int parsedYear) && parsedYear >= 1900 && parsedYear <= 2100)
            {
                year = parsedYear;
            }
            else
            {
                _consoleService.WriteWarning("Invalid year. Using current year.");
            }
        }
    }

    private void DisplayBudgetTable(List<BudgetResponse> budgets)
    {
        _consoleService.WriteLine("| Category        | Limit     | Month/Year | ID (first 8)          |");

        foreach (var budget in budgets)
        {
            string categoryStr = GetCategoryName(budget.Category);
            string idShort = budget.Id.ToString().Substring(0, 8);
            _consoleService.WriteLine($"| {categoryStr,-15} | {budget.MonthlyLimit,9:F2} | {budget.Month:D2}/{budget.Year} | {idShort,-22}|");
        }
    }

    private void DisplayCategories()
    {
        _consoleService.WriteLine("\nCategory:");
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

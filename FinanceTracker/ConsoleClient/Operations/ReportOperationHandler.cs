using FinanceTracker.ConsoleClient.Abstractions;
using FinanceTracker.ConsoleClient.Models;

namespace FinanceTracker.ConsoleClient.Operations;

public class ReportOperationHandler
{
    private readonly IApiClient _apiClient;
    private readonly IConsoleService _consoleService;
    private readonly IUserSession _userSession;

    public ReportOperationHandler(IApiClient apiClient, IConsoleService consoleService, IUserSession userSession)
    {
        _apiClient = apiClient;
        _consoleService = consoleService;
        _userSession = userSession;
    }

    public async Task ViewMonthlySummaryAsync()
    {
        _consoleService.Clear();
        _consoleService.WriteLine("MONTHLY FINANCIAL SUMMARY");

        GetMonthAndYear(out int month, out int year);

        var summary = await _apiClient.GetMonthlySummaryAsync(_userSession.CurrentUserId, month, year);
        if (summary != null)
            DisplaySummary(summary, month, year);

        _consoleService.WaitForKeyPress();
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

    private void DisplaySummary(MonthlySummaryResponse summary, int month, int year)
    {
        string monthName = GetMonthName(month);
        _consoleService.WriteLine($"\nSummary for {monthName} {year}");
        _consoleService.WriteLine($"  Total Income:   ${summary.TotalIncome:F2}");
        _consoleService.WriteLine($"  Total Expense:  ${summary.TotalExpense:F2}");
        _consoleService.WriteLine($"  Net Savings:    ${summary.Savings:F2}");

        if (summary.Savings >= 0)
            _consoleService.WriteSuccess("You are saving money!");
        else
            _consoleService.WriteWarning("You are spending more than earning!");
    }

    private string GetMonthName(int month) => month switch
    {
        1  => "January",
        2  => "February",
        3  => "March",
        4  => "April",
        5  => "May",
        6  => "June",
        7  => "July",
        8  => "August",
        9  => "September",
        10 => "October",
        11 => "November",
        12 => "December",
        _  => "Unknown"
    };
}

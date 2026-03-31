using FinanceTracker.ConsoleClient.Abstractions;

namespace FinanceTracker.ConsoleClient.Controllers;

public class AppController
{
    private readonly IMenuService _menuService;
    private readonly IConsoleService _consoleService;

    public AppController(IMenuService menuService, IConsoleService consoleService)
    {
        _menuService = menuService;
        _consoleService = consoleService;
    }

    public async Task RunAsync()
    {
        try
        {
            await _menuService.RunAsync();
        }
        catch (Exception ex)
        {
            _consoleService.WriteError($"Application error: {ex.Message}");
        }
    }
}

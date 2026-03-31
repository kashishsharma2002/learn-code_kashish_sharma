using FinanceTracker.ConsoleClient.Abstractions;
using FinanceTracker.ConsoleClient.Models;

namespace FinanceTracker.ConsoleClient.Operations;

public class UserOperationHandler
{
    private readonly IApiClient _apiClient;
    private readonly IConsoleService _consoleService;
    private readonly IUserSession _userSession;

    public UserOperationHandler(IApiClient apiClient, IConsoleService consoleService, IUserSession userSession)
    {
        _apiClient = apiClient;
        _consoleService = consoleService;
        _userSession = userSession;
    }

    public async Task CreateNewUserAsync()
    {
        _consoleService.Clear();
        _consoleService.WriteLine("CREATE NEW USER");

        _consoleService.WriteLine("Enter name: ");
        string? name = _consoleService.ReadLine();

        _consoleService.WriteLine("Enter email: ");
        string? email = _consoleService.ReadLine();

        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(email))
        {
            _consoleService.WriteError("Name and email cannot be empty.");
            _consoleService.WaitForKeyPress();
            return;
        }

        var user = await _apiClient.CreateUserAsync(name, email);
        if (user != null)
        {
            _userSession.SetUser(user.Id, user.Name);
            _consoleService.WriteSuccess($"User '{user.Name}' created successfully! ID: {user.Id}");
        }

        _consoleService.WaitForKeyPress();
    }

    public async Task LoginToExistingUserAsync()
    {
        _consoleService.Clear();
        _consoleService.WriteLine("LOGIN TO EXISTING USER");

        _consoleService.WriteLine("Enter user ID (GUID): ");
        string? userIdInput = _consoleService.ReadLine();

        if (!Guid.TryParse(userIdInput, out Guid userId))
        {
            _consoleService.WriteError("Invalid GUID format.");
            _consoleService.WaitForKeyPress();
            return;
        }

        var user = await _apiClient.GetUserAsync(userId);
        if (user != null)
        {
            _userSession.SetUser(user.Id, user.Name);
            _consoleService.WriteSuccess($"Welcome back, {user.Name}!");
        }
        else
        {
            _consoleService.WriteError("User not found.");
        }

        _consoleService.WaitForKeyPress();
    }

    public void Logout()
    {
        _userSession.ClearUser();
        _consoleService.WriteSuccess("Logged out successfully.");
        _consoleService.WaitForKeyPress();
    }
}

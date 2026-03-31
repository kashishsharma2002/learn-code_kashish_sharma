using FinanceTracker.ConsoleClient.Abstractions;
using FinanceTracker.ConsoleClient.Configuration;
using FinanceTracker.ConsoleClient.Controllers;
using FinanceTracker.ConsoleClient.Menu;
using FinanceTracker.ConsoleClient.Services;
using Microsoft.Extensions.Configuration;

IConfiguration configuration = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
    .Build();

AppSettings settings = configuration.Get<AppSettings>()
    ?? throw new InvalidOperationException("appsettings.json is missing or malformed.");


IConsoleService consoleService = new ConsoleService();
IUserSession userSession = new UserSession();
IApiClient apiClient = new ApiClient(settings.Api.BaseUrl, consoleService);
IMenuService menuService = new MenuService(consoleService, userSession, apiClient);

AppController appController = new AppController(menuService, consoleService);
await appController.RunAsync();

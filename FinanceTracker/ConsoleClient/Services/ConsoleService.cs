using FinanceTracker.ConsoleClient.Abstractions;

namespace FinanceTracker.ConsoleClient.Services;

public class ConsoleService : IConsoleService
{
    public void Clear() => Console.Clear();

    public void WriteLine(string message) => Console.WriteLine(message);

    public void WriteLineColored(string message) => Console.WriteLine(message);

    public void WriteSuccess(string message) => WriteLineColored(message);

    public void WriteError(string message) => WriteLineColored(message);

    public void WriteWarning(string message) => WriteLineColored(message);

    public void WriteInfo(string message) => WriteLineColored(message);

    public string? ReadLine() => Console.ReadLine();

    public void WaitForKeyPress()
    {
        WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }
}

namespace FinanceTracker.ConsoleClient.Abstractions;

public interface IConsoleService
{
    void Clear();
    void WriteLine(string message);
    void WriteLineColored(string message);
    void WriteSuccess(string message);
    void WriteError(string message);
    void WriteWarning(string message);
    void WriteInfo(string message);
    void WaitForKeyPress();

    string? ReadLine();
}

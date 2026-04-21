using System.Text;

namespace ETLProcessorApp.Logging;

public sealed class FileEtlLogger : IEtlLogger
{
    private readonly string logFilePath;
    private readonly StringBuilder logBuffer = new();

    public FileEtlLogger(string logFilePath)
    {
        this.logFilePath = logFilePath;
    }

    public void Log(string message)
    {
        var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        logBuffer.AppendLine($"[{timestamp}] {message}");
    }

    public void Flush()
    {
        var directory = Path.GetDirectoryName(logFilePath);
        if (!string.IsNullOrWhiteSpace(directory))
        {
            Directory.CreateDirectory(directory);
        }

        File.WriteAllText(logFilePath, logBuffer.ToString());
    }
}

namespace ETLProcessorApp.Logging;

public interface IEtlLogger
{
    void Log(string message);

    void Flush();
}
